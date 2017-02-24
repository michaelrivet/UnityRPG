using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

class Inventory : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject creditDisplay;
    GameObject slotPanel;
    ItemDatabase database;
    Pauser pauser;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public GameObject inventoryCanvas;

    private bool inventoryOpen = false;
    private int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    public long credits;

    void Start()
    {
        pauser = GameObject.Find("LevelManager").GetComponent<Pauser>();
        // Setting initial slot position. I'm doing an 8x5 grid, so we're going with these numbers 
        Vector2 slotPosition = new Vector2(-350f, 200f);
        database = new ItemDatabase();
        slotAmount = 40;
        inventoryPanel = GameObject.Find("InventoryPanel");
		creditDisplay = GameObject.Find("CreditDisplay");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;

        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform,false);
            slots[i].transform.localPosition = slotPosition;
            // If it's zero or not the 8th slot, move right.  Otherwise, move down.
            if(i == 0 || (i + 1) % 8 != 0)
                slotPosition = new Vector2(slotPosition.x + 100f, slotPosition.y);
            else
                slotPosition = new Vector2(-350f, slotPosition.y - 100f);
        }

        LoadInventory();

        creditDisplay.GetComponent<Text>().text = credits.ToString();
    }

    void LoadInventory()
    {
        if(GlobalControl.Instance.Items != null)
        {
            foreach(ItemData item in GlobalControl.Instance.Items)
            {
				if(item.item.Stackable)
                	AddItem(item.item.Id, item.amount);
				else
					AddItem(item.item.Id);
            }
        }
        else
        {
            AddItem(0);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(1);
            AddItem(2);
            AddItem(2);
        }
    }

    public void SaveInventory()
    {
        GlobalControl.Instance.Items = new List<ItemData>();

		for(int i = 0; i < items.Count; i++)
        {
			if (items[i].Id != -1)
			{
				ItemData id = slots[i].transform.GetChild(1).gameObject.GetComponent<ItemData>();
				if(id != null)
					GlobalControl.Instance.Items.Add(id);
			}
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryOpen)
            pauser.Pause();
        else
            pauser.Unpause();
        inventoryCanvas.GetComponent<Canvas>().enabled = !inventoryOpen;
        inventoryOpen = !inventoryOpen;
    }

    public void AddCredits(long c)
    {
        credits += c;
    }

    public void RemoveCredits(long c)
    {
        credits -= c;
    }

    public bool AddItem(int id, int qty)
    {
        for(int i = 0; i < qty; i++)
        {
            if(!AddItem(id))
			{
				return false;
			}
        }
		DisplayMessage (id, true, qty);
		return true;
    }

    public bool AddItem(int id)
    {
       	Item itemToAdd = database.FetchItemById(id);
		if (itemToAdd == null)
			return false;
		if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
				if (items[i].Id == id && slots[i].transform.GetChild(1).GetComponent<ItemData>().amount < items[i].MaxStack)
                {
                    // Get the data.  Currently using GetChild(1).  This could change if adding more images, or removing the child image.  If you do that, this will cause issues.
                    ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
					return true;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slotId = i;
                    itemObj.transform.SetParent(slots[i].transform, false);
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = "Item: " + itemToAdd.Title;
                    slots[i].name = "Slot: " + itemToAdd.Title;
					if(itemToAdd.Stackable)
					{
						itemObj.GetComponent<ItemData>().amount = 1;
						itemObj.GetComponent<ItemData>().transform.GetChild(0).GetComponent<Text>().text = "1";
					}
					DisplayMessage (id, true);
					return true;
                }
            }
        }
		DisplayMessage(id, false);
		return false;
    }

	void DisplayMessage(int id, bool success, int? qty = null )
	{
		if (success) 
		{
			if(qty == null)
			{

			}
			else
			{
				int q = qty.Value;
			}
		} 
		else 
		{

		}
	}

    public void UseItem(int id)
    {
        Item itemToRemove = database.FetchItemById(id);
        if (itemToRemove == null)
            return;

        if (!CheckIfItemIsInInventory(itemToRemove))
            return;

        if (itemToRemove.Stackable)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == id)
                {
                    // Get the data.  Currently using GetChild(1).  This could change if adding more images, or removing the child image.  If you do that, this will cause issues.
                    ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    if (data.amount == 0)
                    {
                        Destroy(slots[i].transform.GetChild(1).gameObject);
                        items[i].Id = -1;
                    }

                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == itemToRemove.Id)
                {
					Destroy(slots[i].transform.GetChild(1).gameObject);
                    items[i].Id = -1;
                    break;
                }
            }
        }
    }

    public bool CheckfItemIsInInventory(int itemId)
    {
        for (int i = 0; i < items.Count; i++)
        {
			if (items[i].Id == itemId)
            {
				if(!items[i].Stackable || (items[i].Stackable && slots[i].transform.GetChild(1).GetComponent<ItemData>().amount < items[i].MaxStack))
					return true;
            }
        }

        return false;
    }

    bool CheckIfItemIsInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
			if (items[i].Id == item.Id)
            {
				if(!items[i].Stackable || (items[i].Stackable && slots[i].transform.GetChild(1).GetComponent<ItemData>().amount < items[i].MaxStack))
					return true;
            }
        }

        return false;
    }
}


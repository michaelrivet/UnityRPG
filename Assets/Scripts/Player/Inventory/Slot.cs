using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (inv.playerItems[id].Id == -1)
        {
            inv.playerItems[droppedItem.slotId] = new Item();
            inv.playerItems[id] = droppedItem.item;
            droppedItem.slotId = id;
        }
        else if (droppedItem.slotId != id)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slotId = droppedItem.slotId;
            item.transform.SetParent(inv.playerSlots[droppedItem.slotId].transform);
            item.transform.position = inv.playerSlots[droppedItem.slotId].transform.position;

            droppedItem.slotId = id;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.playerItems[droppedItem.slotId] = item.GetComponent<ItemData>().item;
            inv.playerItems[id] = droppedItem.item;
        }
    }
}
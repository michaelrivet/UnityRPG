using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class ItemDatabase
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    public ItemDatabase()
    {
        string json;
        using (StreamReader r = new StreamReader("Assets/Scripts/Items.json"))
        {
            json = r.ReadToEnd();
        }
        itemData = JsonMapper.ToObject(json);
        ConstructItemDatabase();
    }

    public Item FetchItemById(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].Id == id)
            {
                return database[i];
            }
        }

        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Item newItem = new Item();
            newItem.Id = (int)itemData[i]["id"];
            newItem.Title = itemData[i]["title"].ToString();
            newItem.Value = (int)itemData[i]["value"];
            newItem.Power = (int)itemData[i]["stats"]["power"];
            newItem.Defense = (int)itemData[i]["stats"]["defense"];
            newItem.Vitality = (int)itemData[i]["stats"]["vitality"];
            newItem.Description = itemData[i]["description"].ToString();
            newItem.Stackable = (bool)itemData[i]["stackable"];
			if(newItem.Stackable)
				newItem.MaxStack = (int)itemData[i]["maxStack"];
            newItem.Rarity = (int)itemData[i]["rarity"];
            newItem.Slug = itemData[i]["slug"].ToString();
            newItem.Sprite = Resources.Load<Sprite>("Sprites/Items/" + newItem.Slug);

            database.Add(newItem);
        }
    }
}
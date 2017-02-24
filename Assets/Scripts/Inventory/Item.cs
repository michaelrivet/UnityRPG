using UnityEngine;
using System.Collections;

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defense { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
	public int MaxStack { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item()
    {
        this.Id = -1;
    }
}
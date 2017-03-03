using UnityEngine;

class ItemDrop : MonoBehaviour
{
    public int Id;
    public int qty;

	public void Add()
	{
		Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		if(inventory.AddItem(Id, qty))
			Destroy(gameObject);
	}
}


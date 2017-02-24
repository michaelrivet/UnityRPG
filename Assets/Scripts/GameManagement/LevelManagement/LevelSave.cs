using UnityEngine;
using System.Collections;

public class LevelSave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveLevel()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().SaveInventory();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().SavePlayerData();
    }
}

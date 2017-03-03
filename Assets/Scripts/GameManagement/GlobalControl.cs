using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;

    public GameObject Player;
    public PlayerStats PlayerStats;
    public UpgradeStats UpgradeStats;
    public KeyboardSettings KeyboardSettings;
    public List<ItemData> Items;

	// Use this for initialization
	void Awake () {
	    if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
			KeyboardSettings = new KeyboardSettings();
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

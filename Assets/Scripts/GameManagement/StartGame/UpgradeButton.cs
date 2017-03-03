using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { ShowUpgrades(); });
    }
	
	// Update is called once per frame
	void ShowUpgrades() {
        GameObject.Find("StartCanvas").GetComponent<StartGame>().ShowUpgrades();
    }
}

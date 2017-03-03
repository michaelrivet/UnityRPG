using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { ExitUpgrades(); });
    }
	
	// Update is called once per frame
	void ExitUpgrades()
    {
        GameObject.Find("StartCanvas").GetComponent<StartGame>().ExitUpgrades();
    }
}

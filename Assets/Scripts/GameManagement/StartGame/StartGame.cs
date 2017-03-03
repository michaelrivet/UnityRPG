using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public GameObject[] Classes;

    public GameObject InitialMenu;
    public GameObject SelectClass;
    public GameObject Upgrades;

	// Use this for initialization
	void Start () {
        LoadSaveGames();
        LoadKeyboardBindings();
        LoadUpgradeData();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit"))
        {
            InitialMenu.gameObject.SetActive(false);
            SelectClass.gameObject.SetActive(true);
        }
	}

    void LoadSaveGames()
    {

    }

    void LoadUpgradeData()
    {
        if (GlobalControl.Instance.UpgradeStats == null)
            GlobalControl.Instance.UpgradeStats = new UpgradeStats();
        GlobalControl.Instance.UpgradeStats.SetPlayeUpgrades();
    }

    void LoadKeyboardBindings()
    {
        // If custom keyboard bindings, set them.
        if(false)
        {

        }
        else
        {
            GlobalControl.Instance.KeyboardSettings.SetDefault();
        }
    }

    public void ShowUpgrades()
    {
        SelectClass.gameObject.SetActive(false);
        Upgrades.gameObject.SetActive(true);
    }

    public void ExitUpgrades()
    {
        Upgrades.gameObject.SetActive(false);
        SelectClass.gameObject.SetActive(true);
    }
}

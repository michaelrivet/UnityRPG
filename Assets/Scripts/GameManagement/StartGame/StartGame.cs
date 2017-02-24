using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public GameObject[] Classes;

    public GameObject InitialMenu;
    public GameObject SelectClass;

	// Use this for initialization
	void Start () {
        LoadSaveGames();
        LoadKeyboardBindings();
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
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClassSelectScript : MonoBehaviour {

    public GameObject PlayerClass;

	// Use this for initialization
	void Start () {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { StartGame(); });
	}
	
	// Update is called once per frame
	public void StartGame () {
        GlobalControl.Instance.Player = PlayerClass;
        Application.LoadLevel("randomLevel");
    }
}

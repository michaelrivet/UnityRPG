using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {

    public GameObject PlayerLoader;

	// Use this for initialization
	void Awake () {
        GameObject player = Instantiate(GlobalControl.Instance.Player);
        player.transform.position = PlayerLoader.transform.position;
        GameObject.Find("mainCamera").GetComponent<CameraFollow>().SetPlayer(player);
        
        LoadPlayerStats (player.GetComponent<PlayerControl>().PlayerClass);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadPlayerStats(Class PlayerClass)
	{
		// If this is the first level, load the player stats.
		if (GlobalControl.Instance.PlayerStats == null)
			GlobalControl.Instance.PlayerStats = new PlayerStats ();
		GlobalControl.Instance.PlayerStats.SetPlayerStats (PlayerClass);
	}
}

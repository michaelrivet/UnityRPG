using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UpdateLevelDisplays();
    }
	
	// Update is called once per frame
	void UpdateLevelDisplays () {
        GameObject.Find("SoldierSection").gameObject.GetComponentInChildren<Text>().text = GlobalControl.Instance.UpgradeStats.GetLevel(Class.Soldier).ToString();
        GameObject.Find("DoctorSection").gameObject.GetComponentInChildren<Text>().text = GlobalControl.Instance.UpgradeStats.GetLevel(Class.Doctor).ToString();
        GameObject.Find("TechnicianSection").gameObject.GetComponentInChildren<Text>().text = GlobalControl.Instance.UpgradeStats.GetLevel(Class.Technician).ToString();
    }
}

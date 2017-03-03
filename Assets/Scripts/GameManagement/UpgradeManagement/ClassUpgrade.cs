using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClassUpgrade : MonoBehaviour {

    public Class PlayerClass;

    // Use this for initialization
    void Start() {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate () { UpgradeClass(); });
    }

    // Update is called once per frame
    void UpgradeClass()
    {
        if (GlobalControl.Instance.UpgradeStats.GetLevel(PlayerClass) < 20)
        {
            GlobalControl.Instance.UpgradeStats.UpgradeClass(PlayerClass);
            gameObject.transform.parent.gameObject.GetComponentInChildren<Text>().text = GlobalControl.Instance.UpgradeStats.GetLevel(PlayerClass).ToString();
        }
    }
}

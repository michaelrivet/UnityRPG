using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerEnergy : MonoBehaviour {

    public float RechargeDelay = 2f;
    public int RechargeSpeed = 2;

    private Image _energyBar;                   // Reference to the sprite renderer of the health bar.
    private Vector3 _energyScale;                // The local scale of the health bar initially (with full health).
    private PlayerControl _playerControl;        // Reference to the PlayerControl script.

    private bool _recharging = false;

    // Use this for initialization
    void Start () {
        _playerControl = GetComponent<PlayerControl>();
        _energyBar = GameObject.Find("PlayerEnergyBar").GetComponent<Image>();

        _energyScale = _energyBar.transform.localScale;
        UpdateEnergyBar();
    }
	
	// Update is called once per frame
	void Update () {
        if (!_recharging && _playerControl.playerStats.Energy < _playerControl.playerStats.MaxEnergy && Time.time > _playerControl.playerStats.LastSkillTime + RechargeDelay)
        {
            _recharging = true;
            StartCoroutine("RechargeEnergy");
        }
    }

    public void UseEnergy(int Energy)
    {
        _playerControl.playerStats.LastSkillTime = Time.time;
        if (_recharging)
        {
            _recharging = false;
            StopCoroutine("RechargeEnergy");
        }

        _playerControl.playerStats.Energy -= Energy;
        if (_playerControl.playerStats.Energy < 0)
            _playerControl.playerStats.Energy = 0;
        Debug.Log("Using " + Energy + " Energy. (Current Energy: "+ _playerControl.playerStats.Energy + ")");
        UpdateEnergyBar();
    }

    IEnumerator RechargeEnergy()
    {
        while (_playerControl.playerStats.Energy < _playerControl.playerStats.MaxEnergy)
        {
            _playerControl.playerStats.Energy += RechargeSpeed;
            if (_playerControl.playerStats.Energy > _playerControl.playerStats.MaxEnergy)
                _playerControl.playerStats.Energy = _playerControl.playerStats.MaxEnergy;

            UpdateEnergyBar();
            yield return new WaitForSeconds(.1f);
        }
        _recharging = false;
        StopCoroutine("RechargeEnergy");
    }

    void UpdateEnergyBar()
    {
        float energyPercent = (float)_playerControl.playerStats.Energy / (float)_playerControl.playerStats.MaxEnergy;
        Debug.Log("Energy Percent:" + energyPercent);
        _energyBar.transform.localScale = new Vector3(_energyScale.x * energyPercent, 1, 1);
    }
}

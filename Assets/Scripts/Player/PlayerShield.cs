using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShield : MonoBehaviour {

    public float RechargeDelay = 2f;
    public int RechargeSpeed = 2;
    public float RepeatDamagePeriod = 1f;       // How frequently the player can be damaged.
    public AudioClip[] ShieldClips;               // Array of clips to play when the player is damaged.
    public float hurtForce = 10f;               // The force with which the player is pushed when hurt.
    public int damageAmount = 10;               // The amount of damage to take when enemies touch the player

    private Image _shieldBar;            // Reference to the sprite renderer of the health bar.
    private float _lastHitTime;                  // The time at which the player was last hit.
    private Vector3 _shieldScale;                // The local scale of the health bar initially (with full health).
    private PlayerControl _playerControl;        // Reference to the PlayerControl script.
    private Animator _anim;                      // Reference to the Animator on the player

    public bool _recharging = false;
	

    void Start()
    {
        // Setting up references.
        _playerControl = GetComponent<PlayerControl>();
        _shieldBar = GameObject.Find("PlayerShieldBar").GetComponent<Image>();
        _anim = GetComponent<Animator>();

        // Getting the intial scale of the healthbar (whilst the player has full health).
        _shieldScale = _shieldBar.transform.localScale;
        UpdateShieldBar();
    }
	
	// Update is called once per frame
	void Update () {
		if(!_recharging && _playerControl.playerStats.Shield < _playerControl.playerStats.MaxShield && Time.time > _playerControl.playerStats.LastHitTime + RechargeDelay)
        {
            _recharging = true;
			StartCoroutine("RechargeShield");
        }
	}

	public bool IsShieldActive()
    {
        return _playerControl.playerStats.Shield > 0;
    }

    IEnumerator RechargeShield()
    {
        while (_playerControl.playerStats.Shield < _playerControl.playerStats.MaxShield)
        {
            _playerControl.playerStats.Shield += RechargeSpeed;
            if (_playerControl.playerStats.Shield > _playerControl.playerStats.MaxShield)
                _playerControl.playerStats.Shield = _playerControl.playerStats.MaxShield;

            UpdateShieldBar();
            yield return new WaitForSeconds(.1f);
        }
        _recharging = false;
		StopCoroutine("RechargeShield");
    }

    public void DamageShield(int Attack)
    {
		if (_recharging) {
			_recharging = false;
			StopCoroutine ("RechargeShield");
		}

        int damage = DamageCalc.CalcShield(Attack, _playerControl.playerStats.Armor + _playerControl.playerStats.Defense, DamageType.Kinetic);
        _playerControl.playerStats.Shield -= damage;
        if (_playerControl.playerStats.Shield < 0)
            _playerControl.playerStats.Shield = 0;

        // Update what the health bar looks like.
        UpdateShieldBar();

        // Play a random clip of the player getting hurt.
        //int i = Random.Range(0, shieldClips.Length);
        //AudioSource.PlayClipAtPoint(shieldClips[i], transform.position);
    }

    public void UpdateShieldBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        float shieldPercent = (float)_playerControl.playerStats.Shield / (float)_playerControl.playerStats.MaxShield;

        /*if(healthPercent > .5)
			healthBar.color = Color.Lerp(Color.green, Color.yellow, 1 - (healthPercent * 2f - 1f));
		else
			healthBar.color = Color.Lerp(Color.yellow, Color.red, 1 - healthPercent * 2f);*/

        // Set the scale of the health bar to be proportional to the player's health.
        _shieldBar.transform.localScale = new Vector3(_shieldScale.x * shieldPercent, 1, 1);
    }
}

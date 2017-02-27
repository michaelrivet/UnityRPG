using UnityEngine;
using System.Collections;

public class EnemyShield : MonoBehaviour {
    
    public int MaxShield;
    public float RechargeDelay = 2f;
    public int RechargeSpeed = 2;

    public int _shield;
    private bool _recharging = false;
    private IEnumerator _rechargeRoutine;
    private Enemy _enemy;

    private Transform _shieldBar;            // Reference to the sprite renderer of the health bar.
    private Vector3 _shieldScale;                // The local scale of the health bar initially (with full health).

    // Use this for initialization
    void Start ()
    {
        _shieldBar = gameObject.transform.FindChild("ShieldDisplay").FindChild("ShieldBar");
        _shieldScale = _shieldBar.transform.localScale;

        _enemy = gameObject.GetComponent<Enemy>();
        _shield = MaxShield;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsShieldActive()
    {
        return _shield > 0;
    }

    public void DamageShield(int Attack)
    {
        int damage = DamageCalc.CalcShield(Attack, _enemy.Defense, DamageType.Kinetic);
        _shield -= damage;
        if (_shield < 0)
            _shield = 0;

        UpdateShieldBar();
    }

    IEnumerator RechargeShield()
    {
        while (_shield < MaxShield)
        {
            _shield += RechargeSpeed;
            if (_shield > RechargeSpeed)
                _shield = RechargeSpeed;

            UpdateShieldBar();
            yield return new WaitForSeconds(.1f);
        }
        _recharging = false;
        StopCoroutine(_rechargeRoutine);
    }

    public void UpdateShieldBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        float shieldPercent = (float)_shield / (float)MaxShield;

        /*if(healthPercent > .5)
			healthBar.color = Color.Lerp(Color.green, Color.yellow, 1 - (healthPercent * 2f - 1f));
		else
			healthBar.color = Color.Lerp(Color.yellow, Color.red, 1 - healthPercent * 2f);*/

        // Set the scale of the health bar to be proportional to the player's health.
        _shieldBar.localScale = new Vector3(_shieldScale.x * shieldPercent, 1, 1);
    }
}

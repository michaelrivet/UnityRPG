using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int MaxHealth;

    public int _health;
    private Enemy _enemy;

    private Transform _healthBar;            // Reference to the sprite renderer of the health bar.
    private Vector3 _healthScale;                // The local scale of the health bar initially (with full health).
                                                
    // Use this for initialization
    void Start () {
        _healthBar = gameObject.transform.FindChild("HealthDisplay").FindChild("HealthBar");
        _healthScale = _healthBar.transform.localScale;

        _enemy = gameObject.GetComponent<Enemy>();
        _health = MaxHealth;
	}

    public bool IsDamaged()
    {
        return (_health > 0 && _health < (MaxHealth / 2));
    }

    public bool IsDead()
    {
        return _health == 0;
    }

    public void DamageHealth(int damage)
    {
        _health -= damage;
        if (_health < 0)
            _health = 0;

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        float healthPercent = (float)_health / (float)MaxHealth;

        /*if(healthPercent > .5)
			healthBar.color = Color.Lerp(Color.green, Color.yellow, 1 - (healthPercent * 2f - 1f));
		else
			healthBar.color = Color.Lerp(Color.yellow, Color.red, 1 - healthPercent * 2f);*/

        // Set the scale of the health bar to be proportional to the player's health.
        _healthBar.localScale = new Vector3(_healthScale.x * healthPercent, 1, 1);
    }
}

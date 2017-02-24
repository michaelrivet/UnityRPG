using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{	
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public int damageAmount = 10;			    // The amount of damage to take when enemies touch the player

	private Image _healthBar;			// Reference to the sprite renderer of the health bar.
	private Vector3 _healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl _playerControl;		// Reference to the PlayerControl script.
	private Animator _anim;						// Reference to the Animator on the player
    
	void Start ()
	{
		// Setting up references.
		_playerControl = GetComponent<PlayerControl>();
		_healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Image>();
		_anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		_healthScale = _healthBar.transform.localScale;
		UpdateHealthBar ();
	}

   	public void TakeHeal(int health)
    {
        _playerControl.playerStats.Health += health;
        _playerControl.playerStats.Health = Mathf.Clamp(_playerControl.playerStats.Health, 0, 100);
    }

    public void TakeDamage (int damage)
	{
		// Create a vector that's from the enemy to the player with an upwards boost.
		//Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		//GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

        // Reduce the player's health by 10.
		_playerControl.playerStats.Health -= damage;

		if (_playerControl.playerStats.Health < 0)
			_playerControl.playerStats.Health = 0;

		// Update what the health bar looks like.
		UpdateHealthBar();

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}


	public void UpdateHealthBar ()
	{
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        float healthPercent = (float)_playerControl.playerStats.Health / (float)_playerControl.playerStats.MaxHealth;
        
        /*if(healthPercent > .5)
			healthBar.color = Color.Lerp(Color.green, Color.yellow, 1 - (healthPercent * 2f - 1f));
		else
			healthBar.color = Color.Lerp(Color.yellow, Color.red, 1 - healthPercent * 2f);*/

		// Set the scale of the health bar to be proportional to the player's health.
        _healthBar.transform.localScale = new Vector3(_healthScale.x * healthPercent, 1, 1);
	}
}

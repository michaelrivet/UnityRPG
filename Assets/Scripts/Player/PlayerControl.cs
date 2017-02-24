using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public bool facingRight = true;			// For determining which way the player is currently facing.
    public Vector2 movement;

    public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.

	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Animator anim;					// Reference to the player's animator component.
    private Transform t;
    private Rigidbody2D r;

    public PlayerStats playerStats;
	private PlayerHealth _playerHealth;
	private PlayerShield _playerShield;
	public float RepeatDamagePeriod = 1f;       // How frequently the player can be damaged.

    public void SavePlayerData()
    {
        GlobalControl.Instance.PlayerStats = playerStats;
    }

    public void LoadPlayerData()
    {
        playerStats = GlobalControl.Instance.PlayerStats;
    }

	void Awake()
	{
		// Setting up references.
		anim = GetComponent<Animator>();
        t = GetComponent<Transform>();
        r = GetComponent<Rigidbody2D>();
		_playerHealth = GetComponent<PlayerHealth> ();
		_playerShield = GetComponent<PlayerShield> ();
	}

	void Start()
	{
		LoadPlayerData ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "ItemDrop") {
			col.gameObject.GetComponent<ItemDrop>().Add();
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if (col.gameObject.tag == "Enemy" && Time.time > playerStats.LastHitTime + RepeatDamagePeriod)
		{
			Hurt(10);
		}
	}

	public void Hurt(int damage)
	{
		playerStats.LastHitTime = Time.time;
		if (playerStats.Shield > 0) {
			_playerShield.DamageShield (damage);
		} else if (playerStats.Health > damage) {
			_playerHealth.TakeDamage (damage);
		} else {
			Kill ();
		}
	}

	void Kill()
	{

	}

	void FixedUpdate ()
	{
		movement = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));        //Put them in a Vector2 Variable (x,y)
        r.velocity = movement * maxSpeed; 		//Add Velocity to the player ship rigidbody

        //Lock the position in the screen by putting a boundaries
        /*GetComponent<Rigidbody2D>().position = new Vector2 
		(
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),  //X
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)	 //Y
		);*/

		// Logic to store facingRight variable.  only needs set on change
		if(movement.x > 0 && !facingRight)
			facingRight = true;
		else if (movement.x < 0 && facingRight)
			facingRight = false;

		if (!facingRight)
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		else
			transform.localScale = new Vector3 (1f, 1f, 1f);

        float xDir = movement.x == 0f ? 0f : movement.x > 0f ? 1f : -1f;
        float yDir = movement.y == 0f ? 0f : movement.y > 0f ? 1f : -1f;
        if (xDir == 0f && yDir == 0f)
            xDir = facingRight ? 1f : -1f;
        anim.SetFloat("MoveX", xDir);
        anim.SetFloat("MoveY", yDir);
    }

    public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}

	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}

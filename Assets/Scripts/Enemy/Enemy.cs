using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float MoveSpeed = 2f;        // The speed the enemy moves at.
    public int Attack = 5;
    public int Defense = 5;

    private SpriteRenderer _ren;            // Reference to the sprite renderer.
    public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;      // An array of audioclips that can play when the enemy dies.
    private bool _dead = false;         // Whether or not the enemy is dead.

    public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.

    public GameObject Drop;
    public int DroppedQty;
    public GameObject RareDrop;
	public int RareDroppedQty;

	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private Score score;				// Reference to the Score script.

    private EnemyShield _enemyShield;
    private EnemyHealth _enemyHealth;
    private float _lastHit;
	
	void Start()
	{
        _enemyShield = GetComponent<EnemyShield>();
        _enemyHealth = GetComponent<EnemyHealth>();

        // Setting up the references.
        _ren = transform.GetComponent<SpriteRenderer>();
		//score = GameObject.Find("Score").GetComponent<Score>();
	}

	void FixedUpdate ()
	{
		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(_enemyHealth.IsDamaged())
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			_ren.sprite = damagedEnemy;
			
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(_enemyHealth.IsDead() && !_dead)
			// ... call the death function.
			Death ();
	}
	
	public void Hurt(int Attack)
	{
        _lastHit = Time.time;
        // Reduce the number of hit points by one.
        if (_enemyShield.IsShieldActive())
        {
            _enemyShield.DamageShield(Attack);
        }
        else
        {
            _enemyHealth.DamageHealth(Attack);
        }
	}
	
	void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		_ren.enabled = true;
		_ren.sprite = deadEnemy;

		// Increase the score by 100 points
		//score.score += 100;

		// Set dead to true.
		_dead = true;
        
		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		// Play a random audioclip from the deathClips array.
		//int i = Random.Range(0, deathClips.Length);
		//AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);

        DropItem();

        Destroy(gameObject, .5f);
	}

    public void DropItem()
    {
		System.Random r = new System.Random ();

		GameObject item;
		if (r.Next (10) == 0) {
			item = Instantiate (RareDrop);
			item.GetComponent<ItemDrop> ().qty = RareDroppedQty;
		} else {
			item = Instantiate (Drop);
			item.GetComponent<ItemDrop> ().qty = DroppedQty;
		}

		item.transform.position = gameObject.transform.position;
    }

	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}

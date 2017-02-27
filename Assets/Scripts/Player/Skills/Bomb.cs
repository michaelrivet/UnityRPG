using UnityEngine;
using System.Collections;
using System;

public class Bomb : Skill
{
	public float bombRadius = 10f;			// Radius within which enemies are killed.
	public float bombForce = 100f;			// Force that enemies are thrown from the blast.
	public AudioClip boom;					// Audioclip of explosion.
	public AudioClip fuse;					// Audioclip of fuse.
	public float fuseTime = 1f;
	public GameObject explosion;			// Prefab of explosion effect.
    public float Speed = 15f;
    
	private ParticleSystem _explosionFX;		// Reference to the particle system of the explosion effect.
    private Vector2 _direction;

    public override void SetLocation(Vector2 location)
    {
        transform.position = location;
    }

    public override void SetDirection(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * direction.x, Speed * direction.y);
        gameObject.GetComponent<Rigidbody2D>().rotation = (float)(Math.Atan2(direction.y, direction.x) / (2 * Math.PI) * 360f);
    }

    void Awake ()
	{
		// Setting up references.
		/*explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		if(GameObject.FindGameObjectWithTag("Player"))
			layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();*/
	}

	void Start ()
	{
		
		// If the bomb has no parent, it has been laid by the player and should detonate.
		if(transform.root == transform)
			StartCoroutine(BombDetonation());
	}


	IEnumerator BombDetonation()
	{
		// Play the fuse audioclip.
		//AudioSource.PlayClipAtPoint(fuse, transform.position);

		// Wait for 2 seconds.
		yield return new WaitForSeconds(fuseTime);

		// Explode the bomb.
		Explode();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((!_isEnemy && col.tag == "Enemy") || (_isEnemy && col.tag == "Player"))
        {
            StopCoroutine(BombDetonation());
            Explode();
        }
        else if (col.tag != "Player" && col.tag != "Enemy" && col.tag != "ItemDrop" && col.tag != "Bullet")
        {
            StopCoroutine(BombDetonation());
            Explode();
        }
    }

    public void Explode()
	{
		
		// The player is now free to lay bombs when he has them.
		//layBombs.bombLaid = false;
        
		// Find all the colliders on the Enemies layer within the bombRadius.
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRadius, 1 << LayerMask.NameToLayer("Enemies"));

		// For each collider...
		foreach(Collider2D en in enemies)
		{
			// Check if it has a rigidbody (since there is only one per enemy, on the parent).
			Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
			if(rb != null && !_isEnemy && rb.tag == "Enemy")
			{
                rb.gameObject.GetComponent<Enemy>().Hurt((int)(_attack * DamageMult));

                // Find a vector from the bomb to the enemy.
                Vector3 deltaPos = rb.transform.position - transform.position;

				// Apply a force in this direction with a magnitude of bombForce.
				Vector3 force = deltaPos.normalized * bombForce;
				rb.AddForce(force);
			}
		}

		// Set the explosion effect's position to the bomb's position and play the particle system.
		//explosionFX.transform.position = transform.position;
		//explosionFX.Play();

		// Instantiate the explosion prefab.
		Instantiate(explosion,transform.position, Quaternion.identity);

		// Play the explosion sound effect.
		//AudioSource.PlayClipAtPoint(boom, transform.position);

		// Destroy the bomb.
		Destroy (gameObject);
	}
}

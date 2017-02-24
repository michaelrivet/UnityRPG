using UnityEngine;
using System.Collections;
using System;

public class Rocket : MonoBehaviour, IProjectile
{
	public GameObject explosion;		// Prefab of explosion effect.
	public float Speed;

	private bool _isEnemy = false;
	
	public void SetLocation(Vector2 location)
	{
		transform.position = location;
	}

    public void SetDirection(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * direction.x, Speed * direction.y);
        gameObject.GetComponent<Rigidbody2D>().rotation = (float)(Math.Atan2(direction.y, direction.x) / (2 * Math.PI) * 360f);
	}
	
	public void SetIsEnemy (bool isEnemy)
	{
		_isEnemy = isEnemy;
	}

	public bool GetIsEnemy()
	{
		return _isEnemy;
	}
    
    void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}

	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(!_isEnemy && col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Hurt();

			// Call the explosion instantiation.
			OnExplode();

			// Destroy the rocket.
			Destroy (gameObject);
		}
		// If it hits a player...
		else if(_isEnemy && col.tag == "Player")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<PlayerControl>().Hurt(10);
			
			// Call the explosion instantiation.
			OnExplode();
			
			// Destroy the rocket.
			Destroy (gameObject);
		}
		//
		else if(col.tag != "Player" && col.tag != "Enemy" && col.tag != "ItemDrop")
		{
			// Instantiate the explosion and destroy the rocket.
			OnExplode();
			Destroy (gameObject);
		}
	}
}

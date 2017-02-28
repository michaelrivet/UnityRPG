using UnityEngine;
using System.Collections;
using System;

public class Rocket : Projectile
{
	public GameObject explosion;		// Prefab of explosion effect.
	public float Speed;
	
	public override void SetDirection(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * direction.x, Speed * direction.y);
        gameObject.GetComponent<Rigidbody2D>().rotation = (float)(Math.Atan2(direction.y, direction.x) / (2 * Math.PI) * 360f);
	}
    
    void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}

	protected override void OnDestroy()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}	
}

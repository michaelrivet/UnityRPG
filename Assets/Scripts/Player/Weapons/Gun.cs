using UnityEngine;
using System.Collections;
using System;

public class Gun : MonoBehaviour
{
	public GameObject Rocket;				// Prefab of the rocket. (Must be an IProjectile)
	public float Speed = 20f;				// The speed the rocket will fire at.
    
	private PlayerControl _playerCtrl;		// Reference to the PlayerControl script.
	private Animator _anim;					// Reference to the Animator component.

	void Awake()
	{
		// Setting up the references.
		_anim = transform.root.gameObject.GetComponent<Animator>();
        _playerCtrl = transform.root.GetComponent<PlayerControl>();
	}


	void Update ()
	{


		/*if (!_playerCtrl.facingRight)
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		else
			transform.localScale = Vector3.one;*/

		// If the fire button is pressed...
		if(Input.GetButtonDown(GlobalControl.Instance.KeyboardSettings.Fire1))
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			//_anim.SetTrigger("Shoot");
			//GetComponent<AudioSource>().Play();
			
			float xDir = _playerCtrl.movement.x == 0f ? 0f : _playerCtrl.movement.x > 0f ? 1f : -1f;
			float yDir = _playerCtrl.movement.y == 0f ? 0f : _playerCtrl.movement.y > 0f ? 1f : -1f;

            if (xDir == 0f && yDir == 0f)
                xDir = _playerCtrl.facingRight ? 1f : -1f;

            // ... instantiate the rocket facing right and set it's velocity to the right. 
            GameObject bulletInstance = Instantiate(Rocket);
			IProjectile bI = bulletInstance.GetComponent<IProjectile>();

			bI.SetLocation(transform.GetChild(0).gameObject.transform.position);
            bI.SetDirection(new Vector2(xDir, yDir));
		}
	}
}

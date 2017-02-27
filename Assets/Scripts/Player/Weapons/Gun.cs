using UnityEngine;
using System.Collections;
using System;

public class Gun : MonoBehaviour
{
	public GameObject Ammo;				// Prefab of the rocket. (Must be an IProjectile)
	public float Speed = 20f;				// The speed the rocket will fire at.
    
	private Animator _anim;					// Reference to the Animator component.
    private PlayerControl _playerCtrl;

	void Awake()
	{
		// Setting up the references.
		_anim = transform.root.gameObject.GetComponent<Animator>();
        _playerCtrl = transform.root.gameObject.GetComponent<PlayerControl>();
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
            Vector3 mouseLoc = Input.mousePosition;
            mouseLoc.z = 10.0f;
            mouseLoc = Camera.main.ScreenToWorldPoint(mouseLoc);
            
            // ... set the animator Shoot trigger parameter and play the audioclip.
            //_anim.SetTrigger("Shoot");
            //GetComponent<AudioSource>().Play();
            
            Vector2 dir = Vector2.ClampMagnitude(mouseLoc - transform.position, 1.0f);

            GameObject projectileObject = Instantiate(Ammo);
            Projectile p = projectileObject.GetComponent<Projectile>();

            p.SetLocation(transform.GetChild(0).gameObject.transform.position);
            p.SetDirection(new Vector2(dir.x, dir.y));
            p.SetAttack(_playerCtrl.playerStats.BaseAttack + _playerCtrl.playerStats.WeaponAttack);
        }
	}
}

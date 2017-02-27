using UnityEngine;
using System.Collections;
using System;

public class SuppressionFire : Skill {

    public GameObject Projectile;

    public override void SetDirection(Vector2 direction)
    {
        // Suppression fire fires in all directions, so no need to set the direction.
    }

    // Use this for initialization
	void Start () {
        for (int i = 0; i < 8; i++)
        {
            Vector2 direction = Vector2.zero;

            switch(i)
            {
                case 0:
                    direction = Vector2.up;
                    break;
                case 1:
                    direction = Vector2.down;
                    break;
                case 2:
                    direction = Vector2.left;
                    break;
                case 3:
                    direction = Vector2.right;
                    break;
                case 4:
                    direction = Vector2.up + Vector2.left;
                    break;
                case 5:
                    direction = Vector2.up + Vector2.right;
                    break;
                case 6:
                    direction = Vector2.down + Vector2.left;
                    break;
                case 7:
                    direction = Vector2.down + Vector2.right;
                    break;
            }
            GameObject projectileObject = Instantiate(Projectile);
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            projectile.SetLocation(transform.position);
            projectile.SetDirection(direction);
            projectile.SetAttack((int)(_attack * DamageMult));
        }

        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {
    public float DamageMult = 1.0f;
    protected bool _isEnemy = false;
    protected int _attack;

    // Base projectile implementation methods.
    public virtual void SetLocation(Vector2 location)
    {
        transform.position = location;
    }

    public abstract void SetDirection(Vector2 direction);

    public void SetIsEnemy(bool isEnemy)
    {
        _isEnemy = isEnemy;
    }

    public bool GetIsEnemy()
    {
        return _isEnemy;
    }

    public void SetAttack(int Attack)
    {
        _attack = Attack;
    }

    protected abstract void OnDestroy();

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (!_isEnemy && col.tag == "Enemy")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<Enemy>().Hurt((int)(_attack * DamageMult));

            // Call the explosion instantiation.
            OnDestroy();

            // Destroy the rocket.
            Destroy(gameObject);
        }
        // If it hits a player...
        else if (_isEnemy && col.tag == "Player")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<PlayerControl>().Hurt((int)(_attack * DamageMult));

            // Call the explosion instantiation.
            OnDestroy();

            // Destroy the rocket.
            Destroy(gameObject);
        }
        //
        else if (col.tag != "Player" && col.tag != "Enemy" && col.tag != "ItemDrop")
        {
            // Instantiate the explosion and destroy the rocket.
            OnDestroy();
            Destroy(gameObject);
        }
    }
}

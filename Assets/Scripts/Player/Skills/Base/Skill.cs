using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour {
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

}

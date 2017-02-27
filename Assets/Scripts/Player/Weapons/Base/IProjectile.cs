using UnityEngine;

public interface IProjectile
{
    // Use this for initialization
	void SetLocation(Vector2 location);
    void SetDirection(Vector2 direction);
	void SetIsEnemy(bool isEnemy);
	bool GetIsEnemy();
}

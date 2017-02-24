using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float ShootCooldown = 2f;
	public float DodgeCooldown = 1f;
	public GameObject Projectile;
    public Transform target;//set target from inspector instead of looking in Update
    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;
    float speed = 3f;

	public float ShootDistance = 15f;
	public float AggroDistance = 100f;
	private float _lastShotTime;
	private float _lastDodgeTime = 0f;
	private float _distanceToPlayer;
	private Vector2 _dodgeDirection;
	private bool _dodging = false;

	private SpriteRenderer _ren;			// Reference to the sprite renderer.
	private LineRenderer _debugLine;

    void Start()
    {
        enemyRotation = this.transform.localRotation;
		target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		_ren = transform.GetComponent<SpriteRenderer>();
		_debugLine = transform.GetComponent<LineRenderer>();
    }

    void Update()
    {
		if (target == null)
			return;

		_debugLine.SetPosition (0, enemyPos);
		_debugLine.SetPosition (1, playerPos);

        playerPos = new Vector2(target.localPosition.x, target.localPosition.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position
		_distanceToPlayer = Vector3.Distance (transform.transform.position, target.transform.position);

		if(Time.time > _lastDodgeTime + DodgeCooldown)
			IsShotIncoming ();

		if(_dodging)
			_ren.color = Color.blue;

		if (!_dodging &&
		    Vector3.Distance (transform.transform.position, target.transform.position) < AggroDistance &&
			Vector3.Distance (transform.transform.position, target.transform.position) > ShootDistance) {//move towards if not close by
			_ren.color = Color.yellow;
			transform.position = Vector2.MoveTowards (enemyPos, playerPos, 2 * speed * Time.deltaTime);
		}
        /*if (Vector3.Distance(transform.transform.position, target.transform.position) < 1.55)//move away if too close 
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos, -1 * speed * Time.deltaTime);
        }*/

		else if (!_dodging &&
		         Vector3.Distance (transform.transform.position, target.transform.position) <= ShootDistance) {
			_ren.color = Color.red;
			if(Time.time > _lastShotTime + ShootCooldown)
			{
				_lastShotTime = Time.time;
				Shoot ();
			}
		} else if(!_dodging) {
			_ren.color = Color.white;
		}

        if (target.position.x > transform.position.x)//rotates enemy to the right if player is to the right  
        {
            enemyRotation.x = 180;
            transform.localRotation = enemyRotation;
        }
        if (target.position.x < transform.position.x)//rotates enemy to the left if player is to the left 
        {
            enemyRotation.x = 0;
            transform.localRotation = enemyRotation;
        }
    }

	void IsShotIncoming()
	{
		//Collider2D[] shots = Physics2D.OverlapCircleAll(transform.position, 15f, 1 << LayerMask.NameToLayer("Projectiles"));
		RaycastHit2D[] shots = Physics2D.RaycastAll (transform.position, Vector2.ClampMagnitude(playerPos - enemyPos, 1.0f), 200f);
		foreach (RaycastHit2D shot in shots) {
			if(shot.collider.tag == "Bullet")
			{
				IProjectile sI = shot.collider.gameObject.GetComponent<IProjectile>();
				if(!sI.GetIsEnemy()) 
				{
					_lastDodgeTime = Time.time;
					_dodging = true;
					// TODO: Pick random dodge location, make dodge have chance
					_dodgeDirection = Vector2.right;
					StartCoroutine("Dodge");
				}
			}
		}
	}

	IEnumerator Dodge()
	{
		while (Time.time < _lastDodgeTime + .5f)
		{
			transform.position = new Vector2 (transform.position.x + (_dodgeDirection.x * 2 * speed * Time.deltaTime),
			                                                          transform.position.y + (_dodgeDirection.y * 2 * speed * Time.deltaTime));
			yield return null;
		}
		_dodging = false;
		StopCoroutine("Dodge");
	}

	void Shoot() 
	{
		GameObject bulletInstance = Instantiate(Projectile);
		IProjectile bI = bulletInstance.GetComponent<IProjectile>();
		
		bI.SetLocation(transform.position);
		Vector2 dir = Vector2.ClampMagnitude(playerPos - enemyPos, 1.0f);
		Vector2 offset = new Vector2 ((float)Random.Range (0, 30) / 300f, (float)Random.Range (0, 30) / 300f);
		bI.SetDirection(Vector2.ClampMagnitude(dir + offset, 1.0f));
		bI.SetIsEnemy (true);
	}
}

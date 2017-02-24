using UnityEngine;
using System.Collections;
using System;

public class EnemyAI : MonoBehaviour
{
	public float ShootCooldown = 2f;
	public float DodgeCooldown = 1f;
	public GameObject Projectile;
    public Transform target;//set target from inspector instead of looking in Update
    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;
    public float Speed = 3f;
    public float DodgeSpeed = 10f;

	public float ShootDistance = 15f;
	public float AggroDistance = 100f;
	private float _lastShotTime;
	private float _lastDodgeTime = 0f;
    private float _lastMoveTime = 0f;
	private float _distanceToPlayer;

    private Vector2 _dodgeDirection;
    private Vector2 _moveLocation;

    // State bools
	private bool _dodging = false;
    private bool _moving = false;
    private bool _shooting = false;

	private SpriteRenderer _ren;			// Reference to the sprite renderer.
	//private LineRenderer _debugLine;
    
    void Start()
    {
        enemyRotation = this.transform.localRotation;
		target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		_ren = transform.GetComponent<SpriteRenderer>();
		//_debugLine = transform.GetComponent<LineRenderer>();
    }

    void Update()
    {
		if (target == null)
			return;

        // Debug stuff to draw line of sight to player
        //_debugLine.SetPosition(0, enemyPos);
        //_debugLine.SetPosition(1, playerPos);

        playerPos = new Vector2(target.position.x, target.position.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position
        _distanceToPlayer = Vector3.Distance (transform.transform.position, target.transform.position);

        // If we're not dodging or moving
        if(!_dodging && !_moving)
        {
            // First, check if a shot is incoming.  If it is, this will set _dodging = true
            if (Time.time > _lastDodgeTime + DodgeCooldown)
                IsShotIncoming();

            // Next, if we're in AggroDistance
            if (_distanceToPlayer < AggroDistance)
            {
                // If we've gotten too close, move back some
                /*if (Vector3.Distance(transform.transform.position, target.transform.position) < 1.55)//move away if too close 
                {
                    transform.position = Vector2.MoveTowards(enemyPos, playerPos, -1 * speed * Time.deltaTime);
                }*/

                // If we're not in shot distance, chase
                if (Vector3.Distance(transform.transform.position, target.transform.position) > ShootDistance)
                {
                    _ren.color = Color.yellow;
                    transform.position = Vector2.MoveTowards(enemyPos, playerPos, 2 * Speed * Time.deltaTime);
                }

                // If we're in shoot distance
                else
                {
                    _ren.color = Color.red;
                    // If we haven't shot since the cooldown
                    if (Time.time > _lastShotTime + ShootCooldown)
                    {
                        ShootCooldown = UnityEngine.Random.Range(2f, 4f);
                        _lastShotTime = Time.time;
                        Shoot();
                    }
                    else if(Time.time > _lastMoveTime + 2f)
                    {
                        _lastMoveTime = Time.time;
                        if (UnityEngine.Random.Range(0, 3) == 0 )
                        {
                            _moving = true;
                            Vector2 moveLocation = new Vector2(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 4f));
                            _moveLocation = enemyPos + moveLocation;
                            StartCoroutine("Move");
                        }
                    }
                }
            }
                                    
            else
            {
                _ren.color = Color.white;
            }
        }
        // If we're dodging
        else
        {
            _ren.color = Color.blue;
        }
        
    }

	void IsShotIncoming()
	{
        Vector2[] areaVectors = DirectionCalc.GetEnemyCollisionVectors(Vector2.ClampMagnitude(playerPos - enemyPos, 1.0f));
        Collider2D[] Shots = Physics2D.OverlapAreaAll (transform.position - (Vector3)areaVectors[0], transform.position - (Vector3)areaVectors[1]);

		foreach (Collider2D shot in Shots) {
			if(shot.tag == "Bullet")
			{
				IProjectile sI = shot.gameObject.GetComponent<IProjectile>();
				if(!sI.GetIsEnemy()) 
				{
					_lastDodgeTime = Time.time;
                    if (UnityEngine.Random.Range(0, 3) == 0  )
                    {
                        _dodging = true;
                        _dodgeDirection = DirectionCalc.GetEnemyDodgeDirection(Vector2.ClampMagnitude((Vector2)shot.transform.position - (Vector2)transform.position, 1.0f));

                        StartCoroutine("Dodge");
                    }
				}
			}
		}
	}

	IEnumerator Dodge()
    {
        while (Time.time < _lastDodgeTime + .3f)
        {
            transform.position = new Vector2(transform.position.x + (_dodgeDirection.x * 2 * DodgeSpeed * Time.deltaTime),
                                                                      transform.position.y + (_dodgeDirection.y * 2 * DodgeSpeed * Time.deltaTime));
            yield return null;
        }
        _dodging = false;
		StopCoroutine("Dodge");
	}

    IEnumerator Move()
    {
        while (Vector3.Distance(transform.transform.position, _moveLocation) > 0.2f && Time.time < _lastMoveTime + 1f)
        {
            _ren.color = Color.cyan;
            transform.position = Vector2.MoveTowards(enemyPos, _moveLocation, 2 * Speed * Time.deltaTime);

            yield return null;
        }

        _moving = false;
        StopCoroutine("Move");
    }

    void Shoot() 
	{
		GameObject bulletInstance = Instantiate(Projectile);
		IProjectile bI = bulletInstance.GetComponent<IProjectile>();
		
		bI.SetLocation(transform.position);
		Vector2 dir = Vector2.ClampMagnitude(playerPos - enemyPos, 1.0f);
		//Vector2 offset = new Vector2 ((float)UnityEngine.Random.Range (0, 30) / 300f, (float)UnityEngine.Random.Range (0, 30) / 300f);
		bI.SetDirection(Vector2.ClampMagnitude(dir , 1.0f));
		bI.SetIsEnemy (true);
	}
}
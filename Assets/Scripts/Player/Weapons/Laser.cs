using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour, IProjectile {
	
	public Transform LaserHit;
	public float Distance = 20f;
	public int Damage;

	private LineRenderer _lineRenderer;
	private Vector2 _direction;
    private bool _firstHit = true;
	private bool _isEnemy;

	public void SetLocation(Vector2 location)
	{
		transform.position = location;
	}
	
	public void SetDirection(Vector2 direction)
	{
		_direction = direction;
	}

	public void SetIsEnemy (bool isEnemy)
	{
		_isEnemy = isEnemy;
	}
	
	public bool GetIsEnemy()
	{
		return _isEnemy;
	}

	// Use this for initialization
	void Start () {
		_lineRenderer = GetComponent<LineRenderer> ();
		_lineRenderer.useWorldSpace = true;

		_lineRenderer.enabled = true;
		Destroy(gameObject, .5f);
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D finalHit = new RaycastHit2D();
		finalHit.point = Vector2.zero;
		RaycastHit2D[] hitSet = Physics2D.RaycastAll (transform.position, _direction);
        if (_firstHit)
        {
            foreach (RaycastHit2D hit in hitSet)
            {
                if (hit.collider)
                {
					if (!_isEnemy && hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().Hurt();
                        finalHit = hit;
                        LaserHit.position = transform.position;
                    }
                }
            }
            _firstHit = false;
        }

		if (finalHit.point == Vector2.zero)
			finalHit.point = new Vector2 (transform.position.x + (_direction.x * Distance), transform.position.y + (_direction.y * Distance));
		LaserHit.position = finalHit.point;
		
		_lineRenderer.SetPosition(0, transform.position);
		_lineRenderer.SetPosition(1, LaserHit.position);
	}

}

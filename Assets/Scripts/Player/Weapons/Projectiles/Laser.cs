using UnityEngine;
using System.Collections;
using System;

public class Laser : Projectile {
	
	public Transform LaserHit;
	public float Distance = 80f;
	public int Damage;

	private LineRenderer _lineRenderer;
	private Vector2 _direction;
    private bool _firstHit = true;

    private bool _expanding = true;
    private float _width = .2f;
	
	public override void SetDirection(Vector2 direction)
	{
		_direction = direction;
	}

	// Use this for initialization
	void Start () {
		_lineRenderer = GetComponent<LineRenderer> ();
		_lineRenderer.useWorldSpace = true;
        _lineRenderer.sortingLayerName = "Foreground";

		_lineRenderer.enabled = true;
		Destroy(gameObject, .5f);
        StartCoroutine(ExpandContract());
	}

    IEnumerator ExpandContract()
    {
        while(true)
        {
            // Logic to make laser expand/contract
            if (_expanding)
            {
                _width += .5f;
                _lineRenderer.SetWidth(_width, _width);
                if (_width >= 2.0f)
                    _expanding = false;
            }
            else
            {
                _width -= 0.2f;
                if (_width < 0f)
                    _width = 0.2f;
                _lineRenderer.SetWidth(_width, _width);
            }
            yield return new WaitForSeconds(.01f); ;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

		// Only need to do collision check on first hit.
        if (_firstHit)
        {
			Vector2 finalHit = Vector2.zero;

			RaycastHit2D[] hitSet = Physics2D.RaycastAll (transform.position, _direction, Distance);

            foreach (RaycastHit2D hit in hitSet)
            {
                if (hit.collider && finalHit == Vector2.zero)
                {
					if (!_isEnemy && hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().Hurt((int)(_attack * DamageMult));
                    }

                    else if(_isEnemy && hit.collider.gameObject.tag == "Player")
                    {
                        hit.collider.gameObject.GetComponent<PlayerControl>().Hurt((int)(_attack * DamageMult));
                        
                    }
					if(hit.collider.gameObject.tag == "Wall")
					{
						finalHit = hit.point;
					}
                }
            }
            _firstHit = false;

            if (finalHit == Vector2.zero)
                finalHit = Vector2.ClampMagnitude(new Vector2(transform.position.x + (_direction.x * Distance), transform.position.y + (_direction.y * Distance)), Distance);
            LaserHit.position = finalHit;

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, LaserHit.position);
        }

	}

    protected override void OnDestroy()
    {
        // Laser don't do anything on destory.  OnDestroy isn't necessary
    }
}

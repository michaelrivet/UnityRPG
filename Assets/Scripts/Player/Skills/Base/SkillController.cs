using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour {

    public string SkillName;
    public GameObject SkillTimer;
    public float Cooldown = 5.0f;
    public GameObject SkillObject;
    public int BaseDamage;
    public int SkillId;

    private bool _canUse = true;
	private float _countdownTime = 0;
	private string _keyCode;
    private PlayerControl _playerCtrl;      // Reference to the PlayerControl script.
    private Animator _anim;					// Reference to the Animator component.

    // Use this for initialization
    void Start () {
        _keyCode = GlobalControl.Instance.KeyboardSettings.GetSkillKey(SkillId);
        _playerCtrl = GetComponent<PlayerControl>();
        _anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(!_canUse)
        {
            _countdownTime -= Time.deltaTime;
            if(_countdownTime < 0)
            {
                _countdownTime = 0;
                _canUse = true;
            }
            UpdateSkillTimer();
        }
        else
        {
            if(Input.GetButtonDown(_keyCode))
            {
                _canUse = false;
                _countdownTime = Cooldown;

                float xDir = _playerCtrl.movement.x == 0f ? 0f : _playerCtrl.movement.x > 0f ? 1f : -1f;
                float yDir = _playerCtrl.movement.y == 0f ? 0f : _playerCtrl.movement.y > 0f ? 1f : -1f;
                if (xDir == 0f && yDir == 0f)
                    xDir = _playerCtrl.facingRight ? 1f : -1f;

                GameObject skillInstance = Instantiate(SkillObject);
                Skill s = skillInstance.GetComponent<Skill>();
                s.SetLocation(transform.position);
                s.SetDirection(new Vector2(xDir, yDir));
                s.SetAttack(_playerCtrl.playerStats.BaseAttack);
            }
        }
	}

    void UpdateSkillTimer()
    {

    }
}

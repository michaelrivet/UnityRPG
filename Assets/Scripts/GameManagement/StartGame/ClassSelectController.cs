using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClassSelectController : MonoBehaviour {

    private bool _isControllerEnabled = false;
    public int _selected = 0;
    private List<GameObject> _classes;
    private Image _selectedImage;
	private bool _isJoystick = false;
	private int _joystickDelay = 20;
	private int _currentJoystickDelay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(
            !_isControllerEnabled &&
	            (
					Input.GetButtonDown("Fire1") ||
		            Input.GetButtonDown("Fire2") ||
		            Input.GetButtonDown("Submit") ||
		            Input.GetAxis("Horizontal") > 0f ||
		            Input.GetAxis("Vertical") > 0f
				)
            )
        {
            _isControllerEnabled = true;
            _selectedImage = gameObject.GetComponent<Image>();
            _selectedImage.enabled = true;
			_isJoystick = true;
			_currentJoystickDelay = _joystickDelay;

            _classes = new List<GameObject>();
            Transform tClasses = GameObject.Find("Classes").transform;
            foreach (Transform child in tClasses)
            {
                _classes.Add(child.gameObject);
            }
        }

        else if(_isControllerEnabled)
        {
            if(Input.GetButtonDown("Submit"))
            {
                GlobalControl.Instance.Player = _classes[_selected].GetComponent<ClassSelectScript>().PlayerClass;
                Application.LoadLevel("randomLevel");
            }
            if(Input.GetAxis("Horizontal") > 0f)
            {
				if(!_isJoystick || _currentJoystickDelay == 0)
				{
					_isJoystick = true;
					_currentJoystickDelay = _joystickDelay;
					
					if(_selected + 1 < _classes.Count)
					{
						_selected++;
						_selectedImage.transform.localPosition = new Vector3(_selectedImage.transform.localPosition.x + 320f, _selectedImage.transform.localPosition.y);
						
					}
					else
					{
						_selected = 0;
						_selectedImage.transform.localPosition = new Vector3(-320f, _selectedImage.transform.localPosition.y);
					}
				}
				else
				{
					_currentJoystickDelay --;
				}
            }
			else if(Input.GetAxis("Horizontal") < 0f)
            {
				if(!_isJoystick || _currentJoystickDelay == 0)
				{
					_isJoystick = true;
					_currentJoystickDelay = _joystickDelay;

					if (_selected > 0)
					{
						_selected--;
						_selectedImage.transform.localPosition = new Vector3(_selectedImage.transform.localPosition.x - 320f, _selectedImage.transform.localPosition.y);
						
					}
					else
					{
						_selected = _classes.Count - 1;
						_selectedImage.transform.localPosition = new Vector3(320f, _selectedImage.transform.localPosition.y);
					}
				}
				else 
				{
					_currentJoystickDelay --;
				} 
            }
			else if(Input.GetAxis("Horizontal") == 0f)
	        {
				_isJoystick = false;
			}
        }
	}
}

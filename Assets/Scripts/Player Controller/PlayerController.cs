using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private Xbox360Controller _gamepad = new Xbox360Controller(Controller.PlayerNumbers.Player1);
    public Xbox360Controller Gamepad {get { return _gamepad; } set { _gamepad = value; }}

    [SerializeField]
    private float _minimumForwardSpeed = 2.0f;
    
    [SerializeField]
    private float _maximumForwardSpeed = 20.0f;

    private float _currentForwardSpeed;
    private float _currentLateralSpeed = 0.0f;

	[SerializeField]
    private const float MAX_LATERAL_SPEED = 20.50f;

	[SerializeField]
    private float _decelerationRate = .5f;

	[SerializeField]
	private float _jumpImpulse = 5f;
    
    private Transform previousTransform;

    [SerializeField]
    private bool _isAirborne = false;

    [SerializeField]
    private bool _canUseDoubleJump = true;

    private bool _isWallRunning = false;
    private float _timeWallRunning = 0.0f;

    private ContactPoint _lastContactPoint;

    private int _wallID = 0;

    private void Awake()
    {
        _currentForwardSpeed = _minimumForwardSpeed;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!GameManager.Instance.IsPaused) ;

	    if (Mathf.Abs(Gamepad.LeftThumbstickY) > 0.0f)
	    {
	        _currentForwardSpeed += Gamepad.LeftThumbstickY * Time.deltaTime;
	    }
	    else
	    {
	        _currentForwardSpeed += Input.GetAxis("Vertical") * Time.deltaTime;
	    }

	    if (_currentForwardSpeed < _minimumForwardSpeed)
	    {
	        _currentForwardSpeed = _minimumForwardSpeed;
	    }
        else if (_currentForwardSpeed > _maximumForwardSpeed)
        {
            _currentForwardSpeed = _maximumForwardSpeed;
        }

	    if (Mathf.Abs(Gamepad.LeftThumbstickX) > 0.0f)
	    {
	        _currentLateralSpeed = MAX_LATERAL_SPEED * Gamepad.LeftThumbstickX; // * Time.deltaTime;
	    }
	    else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f)
	    {
	        _currentLateralSpeed = MAX_LATERAL_SPEED * Input.GetAxis("Horizontal"); // * Time.deltaTime;
	    }
        //else
        //{
        //    _currentLateralSpeed -= (_currentLateralSpeed * _decelerationRate * Time.deltaTime);
        //}
        if (_isAirborne && _canUseDoubleJump && Input.GetKeyDown(KeyCode.Space))
	    {
            _canUseDoubleJump = false;
            if (!_isWallRunning)
	        {
	            gameObject.rigidbody.AddForce(new Vector3(0, _jumpImpulse, 0), ForceMode.Impulse);
	        }
	        if (_isWallRunning)
	        {
                gameObject.rigidbody.AddForce(5 * (new Vector3(0, 1, 0) + 2 * _lastContactPoint.normal), ForceMode.Impulse);
            }
	    }
	    if (!_isAirborne && Input.GetKeyDown(KeyCode.Space))
	    {
	        _isAirborne = true;
	        gameObject.rigidbody.AddForce(new Vector3(0, _jumpImpulse, 0), ForceMode.Impulse);
	    }


	    if (_isWallRunning)
	    {
	        _timeWallRunning += Time.deltaTime;
	    }

        if (_timeWallRunning >= 2.0f)
        {
            _isWallRunning = false;
            _timeWallRunning = 0.0f;
            gameObject.rigidbody.AddForce(_lastContactPoint.normal, ForceMode.Impulse);
        }


	    Vector3 movementVelocity = new Vector3(_currentLateralSpeed, 0.0f, _currentForwardSpeed);

        gameObject.rigidbody.MovePosition(gameObject.transform.position + movementVelocity * Time.deltaTime);

	    previousTransform = gameObject.transform;
	}

    private void OnCollisionEnter(Collision otherObject)
    {
        Debug.Log("Collision enter");
        _isAirborne = false;
        _canUseDoubleJump = true;

        if (otherObject.gameObject.tag == "Wall")
        {
            _wallID = otherObject.gameObject.GetInstanceID();
            _isWallRunning = true;
            gameObject.rigidbody.useGravity = false;
        }

        _lastContactPoint = otherObject.contacts[0];
    }

    private void OnCollisionExit(Collision otherObject)
    {
        if (otherObject.gameObject.tag == "Wall")
        {
            _isWallRunning = false;
            _timeWallRunning = 0.0f;
            gameObject.rigidbody.useGravity = true;
        }
    }

    private void OnCollisionStay(Collision otherObject)
    {
        if (otherObject.gameObject.tag == "Wall")
        {
            _lastContactPoint = otherObject.contacts[0];
        }
    }
}

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

	private int _wallLeaveTimer = 0;

	private int _wallLeaveTime = 20;

	private bool _wallLeaveTimerActive = false;

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
		if(_wallLeaveTimerActive)
		{
			_wallLeaveTimer++;
			if(_wallLeaveTimer > _wallLeaveTime)
			{
				_wallLeaveTimerActive = false;
				_wallLeaveTimer = 0;
			}
		}

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

		if (Mathf.Abs(Gamepad.LeftThumbstickX) > 0.1f && !_isWallRunning)
	    {
	        _currentLateralSpeed = MAX_LATERAL_SPEED * Gamepad.LeftThumbstickX; // * Time.deltaTime;
	    }
	    else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && !_isWallRunning)
	    {
			// for now, must not let player move off wall run until reliable exit code
	        _currentLateralSpeed = MAX_LATERAL_SPEED * Input.GetAxis("Horizontal"); // * Time.deltaTime;
	    }
		else
			_currentLateralSpeed = 0;
        //else
        //{
        //    _currentLateralSpeed -= (_currentLateralSpeed * _decelerationRate * Time.deltaTime);
        //}
        if (_isAirborne && _canUseDoubleJump && Input.GetKeyDown(KeyCode.Space))
	    {
            _canUseDoubleJump = false;
            if (!_isWallRunning)
	        {
				StartCoroutine("Jump");
	        }
	        if (_isWallRunning)
	        {
				_isAirborne = true;
				_isWallRunning = false;
				_timeWallRunning = 0.0f;
				Debug.Log("Gravity enabled");
				gameObject.rigidbody.useGravity = true;
				_wallLeaveTimerActive = true;
				gameObject.rigidbody.velocity = Vector3.zero;
                gameObject.rigidbody.AddForce((new Vector3(0,_jumpImpulse,0) + _jumpImpulse/3 * _lastContactPoint.normal), ForceMode.Impulse);
            }
	    }
	    if (!_isAirborne && Input.GetKeyDown(KeyCode.Space))
	    {
	        _isAirborne = true;
			StartCoroutine("Jump");
	    }


	    if (_isWallRunning)
	    {
			Vector3 v = rigidbody.velocity;
			v.x = 0;
			v.y = 0;
			rigidbody.velocity = v;

			Vector3 v2 = transform.position;
			// curvy path
			v2.y = (float)Math.Sin(_timeWallRunning*4)/6 + v2.y;
			transform.position = v2;

	        _timeWallRunning += Time.deltaTime;
	    }

        if (_timeWallRunning >= 1f)
        {
			_isAirborne = true;
			Debug.Log("fail");
            _isWallRunning = false;
            _timeWallRunning = 0.0f;
			Debug.Log("Gravity enabled");
			_wallLeaveTimerActive = true;
			gameObject.rigidbody.useGravity = true;

            gameObject.rigidbody.AddForce(_lastContactPoint.normal, ForceMode.Impulse);
        }


	    Vector3 movementVelocity = new Vector3(_currentLateralSpeed, 0.0f, _currentForwardSpeed);

        gameObject.rigidbody.MovePosition(gameObject.transform.position + movementVelocity * Time.deltaTime);

	    previousTransform = gameObject.transform;
	}

	/// <summary>
	/// Jump slowly for first few milliseconds, then ramp up
	/// </summary>
	private IEnumerator Jump()
	{
		float impulse = 0;
		while(true)
		{
			impulse = Mathf.Lerp(impulse, _jumpImpulse, 0.1f);
			if(impulse < _jumpImpulse/2.45f)
			{
				gameObject.rigidbody.AddForce(new Vector3(0, impulse, 0), ForceMode.Impulse);
				yield return null;
			}
			else
			{
				break;
			}
		}
	}

    private void OnCollisionEnter(Collision otherObject)
    {
        Debug.Log("Collision enter");
		if (otherObject.gameObject.tag == "Wall" && !_wallLeaveTimerActive && _isAirborne)
		{
			_wallID = otherObject.gameObject.GetInstanceID();
			_isWallRunning = true;
			Debug.Log("Gravity removed");
			gameObject.rigidbody.useGravity = false;
		}
		else
			_isAirborne = false;

		if (otherObject.gameObject.tag == "Wall")
		{
			_wallLeaveTimerActive = false;
			_wallLeaveTimer = 0;
		}
        _canUseDoubleJump = true;

       

        _lastContactPoint = otherObject.contacts[0];
    }

	/// <summary>
	/// Raises the collision exit event.
	/// </summary>
	/// <param name="otherObject">Other object.</param>
    private void OnCollisionExit(Collision otherObject)
    {
		// will not work. collision exits all the time, not reliable
        /*if (otherObject.gameObject.tag == "Wall")
        {
            _isWallRunning = false;
            _timeWallRunning = 0.0f;
            gameObject.rigidbody.useGravity = true;
        }*/
    }

    private void OnCollisionStay(Collision otherObject)
    {
        if (otherObject.gameObject.tag == "Wall")
        {
            _lastContactPoint = otherObject.contacts[0];
        }
    }
}

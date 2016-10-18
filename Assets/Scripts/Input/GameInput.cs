using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameInput : MonoBehaviour {

	public GameController gameController;
	public static GameGesture gesture;

	private Vector2 _pointerDown;
	private Vector2 _pointerUp;
	private Vector3 _rotation;

	private GameController _gameController;
	private Player _player;

	private int _directionIndex;
	private float _swipeCounter; //This counter is used for keeping track of the time between each swipes.
	private bool _swiped;
	private bool _canSwipe;
	private bool _initSwipe;
	private float _doubleTapDelayTimer;
	private bool _startDoubleTapTimer;
	private bool _doubleTapped;
	void Start()
	{
		_gameController = GetComponent<GameController>();
		_player = _gameController.player.GetComponent <Player>();
		_rotation = Vector3.zero;
		_canSwipe = true;
		EventManager.OnSwipeRight += SwipeRight;
		EventManager.OnSwipeLeft += SwipeLeft;
		EventManager.OnTap += Tap;
		EventManager.OnDoubleTap += DoubleTap;
	}

	void Update()
	{
		if (_gameController.gameState == GameState.GAME)
		{
			if (_swiped)
			{
				_swipeCounter += Time.deltaTime;
			}

			if (_swipeCounter > Constants.SwipeDelay)
			{
				_canSwipe = true;
				_swiped = false;
				_swipeCounter = 0;
			}

			if (_startDoubleTapTimer)
			{
				_doubleTapDelayTimer += Time.deltaTime;
			}

			if (_doubleTapped)
			{
				if (_doubleTapDelayTimer < Constants.DoubleTapDelay)
				{
					Debug.Log("DoubleTap");
					if (EventManager.OnDoubleTap != null)
					{
						EventManager.OnDoubleTap();
					}
				}

				_doubleTapDelayTimer = 0;
				_startDoubleTapTimer = false;
				_doubleTapped = false;
			}
		}
	}

	private void CalculateSwipe()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
		if (_gameController.autoPlay == false)
		{
			if (_abs > Constants.SwipeThresHold)
			{
				_swiped = true;
				_canSwipe = false;
				if (EventManager.OnSwipe != null)
				{
					EventManager.OnSwipe();
				}
				float _difference = -(_pointerDown.x - _pointerUp.x);
				float zRotation = (_difference < 0) ? 90 : -90;
				_player.zRotation += zRotation;
				_player.zRotation %= 360.0f;


				_directionIndex = (_difference > 0) ? _directionIndex +  1 : _directionIndex - 1;
				if (_difference > 0)
				{
					if (EventManager.OnSwipeRight != null)
					{
						EventManager.OnSwipeRight();
					}
				} else if (_difference < 0)
				{
					if (EventManager.OnSwipeRight != null)
					{
						EventManager.OnSwipeLeft();
					}

				}

				if (_directionIndex < 0)
				{
					_directionIndex = Constants.directions.Length - 1;
				} else if (_directionIndex > Constants.directions.Length - 1)
				{
					_directionIndex = 0;
				}
				_gameController.facingDirection = Constants.directions[_directionIndex];
			}
		}

		if (EventManager.OnTap != null)
		{

			if (_startDoubleTapTimer == false)
			{
				_startDoubleTapTimer = true;
			} else {
				_doubleTapped = true;
			}

		}
		_pointerDown = Vector2.zero;
		_pointerUp = Vector2.zero;
	}

	public void RegisterSwipe()
	{
		if (_gameController.gameState == GameState.GAME)
		{

			if (Input.GetMouseButtonDown(0))
			{
				_pointerDown = Input.mousePosition;
				_initSwipe = true;
			}

			if (_initSwipe)
			{
				if (Input.GetMouseButtonUp(0))
				{
					_pointerUp = Input.mousePosition;
					_initSwipe = false;
					if (_canSwipe)
					{
						CalculateSwipe();
					}
				}
			}


		}
	}

	public void RegisterAcceleration()
	{
		Vector3 _acceleration = Vector3.zero;
		Vector3 _offsetPositon = Vector3.zero;
		float _multiplier = 2.0f;
		_acceleration.x = Input.acceleration.x;
		_acceleration.x = Mathf.Round(_acceleration.x * 10f) / 10f;
		Direction _direction = _gameController.facingDirection;
		Transform _rightBound = _gameController.player.transform.FindChild("RightBound");
		Transform _leftBound = _gameController.player.transform.FindChild("LeftBound");
		Transform _center = _gameController.player.transform.FindChild("Center");

		// if direciton is north
		// if acc is +
		// check the distance between the right bound and center
		// that should be your multiplier
		if (_direction == Direction.NORTH)
		{


			_offsetPositon.x = _acceleration.x * _multiplier;
			_offsetPositon.y = _offsetPositon.z = 0;
		}

		_gameController.player.transform.position = _offsetPositon;
		GameObject.Find("GameCanvas").transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + _multiplier;
	}

	private void SwipeLeft()
	{
		gesture = GameGesture.LEFT;

	}
	private void Tap()
	{
		gesture = GameGesture.TAP;
	}
	private void SwipeRight()
	{
		gesture = GameGesture.RIGHT;
	}

	private void DoubleTap()
	{
		gesture = GameGesture.DOUBLETAP;
	}
}

public enum GameGesture
{
	NONE,
	RIGHT,
	LEFT,
	TAP,
	DOUBLETAP,
}


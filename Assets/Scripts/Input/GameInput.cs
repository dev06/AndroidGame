using UnityEngine;
using System.Collections;

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
		Debug.Log(gesture);
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
			if (_doubleTapDelayTimer < 1.0f)
			{
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

	private void CalculateSwipe()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
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

		} else if (EventManager.OnTap != null)
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
		if (gameController.autoPlay == false)
		{
			if (Input.GetMouseButtonDown(0))
			{
				_pointerDown = Input.mousePosition;

			}

			if (Input.GetMouseButtonUp(0))
			{
				_pointerUp = Input.mousePosition;
				if (_canSwipe)
				{
					CalculateSwipe();
				}

			}
		}
	}

	private void RegisterDoubleTap()
	{

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


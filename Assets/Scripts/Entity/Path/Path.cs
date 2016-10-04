using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour
{

	public GameObject previousWall;
	public Vector3 offsetedPosition;
	public bool isCurrentlyPaused;
	private Vector3 _movementDirection;
	private GameController _gameController;
	private GameObject _wallObjects;
	private SpriteRenderer _spriteRenderer;
	private Animation _pathAnimation;
	private bool _wallPassed;
	private bool _enteredWall;
	private bool _shouldDestroy;
	private bool _assignPosition;
	private bool _isPaused;
	private bool _startPoolTimer;
	private bool _onDeath;
	private float _wallSpeed;
	private float _pauseTimer;
	private float _poolTimer;
	private float _maxPoolTimer;
	private float _wallSpeedVel;
	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_wallObjects = GameObject.FindWithTag("WallObjects");
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_movementDirection = Vector3.zero;
		_pathAnimation = GetComponent<Animation>();
		EventManager.OnSwipe += Pause;
		EventManager.OnDeath += OnPlayerDeath;
	}


	void Update ()
	{
		if (_onDeath == false)
		{
			_wallSpeed = Constants.WallSpeed;
		} else
		{
			_wallSpeed = Mathf.SmoothDamp(_wallSpeed, 0, ref _wallSpeedVel, .5f);
		}

		if (_gameController.gameState == GameState.GAME)
		{
			if (_isPaused)
			{
				_pauseTimer += Time.deltaTime;
			} else
			{
				Move(_gameController.facingDirection, _wallSpeed);
			}
			if (_pauseTimer > Constants.SwipePause)
			{
				_isPaused = false;
				_pauseTimer = 0;
			}
		}

	}

	private void Move(Direction _direction, float _velocity)
	{
		if (_gameController.move)
		{
			if (_direction == Direction.EAST)
			{
				_movementDirection.x = -Time.deltaTime * _velocity;
				_movementDirection.y = 0;
			} else if (_direction == Direction.WEST)
			{
				_movementDirection.x = Time.deltaTime * _velocity;
				_movementDirection.y = 0;
			} else if (_direction == Direction.SOUTH)
			{
				_movementDirection.x = 0;
				_movementDirection.y = Time.deltaTime * _velocity;
			} else {
				_movementDirection.x = 0;
				_movementDirection.y = -Time.deltaTime * _velocity;
			}

			transform.position += _movementDirection;
		}
	}

	private void Pause()
	{
		_isPaused = true;
	}

	public void ChangeColor(bool entered)
	{
		if (entered)
		{
			_enteredWall = entered;
		}
		_spriteRenderer.color = (entered) ? new Color(0, 1, 0, 1) : new Color(1, 1, 1, 1);
	}

	public void Animate()
	{
		G_Animation.Play(_pathAnimation, _pathAnimation.clip.name, 1, 1);
	}

	private void ResetUponPool()
	{
		if (_pathAnimation != null)
		{
			_pathAnimation.Stop();
			_pathAnimation[_pathAnimation.clip.name].time = 0;
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		}
	}

	private void OnPlayerDeath()
	{
		_onDeath = true;
	}


}

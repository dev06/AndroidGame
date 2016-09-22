using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

	// Use this for initialization

	public GameObject previousWall;
	public bool move;
	public Vector3 offsetedPosition;

	private GameController _gameController;
	private GameObject _wallObjects;
	private SpriteRenderer _spriteRenderer;
	private bool _wallPassed;
	private bool _enteredWall;
	private bool _shouldDestroy;
	private bool _assignPosition;

	private float _wallSpeed;
	private Vector3 _movementDirection;

	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_wallObjects = GameObject.FindWithTag("WallObjects");
		_spriteRenderer = GetComponent<SpriteRenderer>();


		_wallSpeed = Constants.WallSpeed;
		_movementDirection = Vector3.zero;
	}


	void Update ()
	{


		Move(_gameController.facingDirection);

	}


	private void Move(Direction _direction)
	{

		if (move)
		{
			if (_direction == Direction.EAST)
			{
				_movementDirection.x = -Time.deltaTime * _wallSpeed;
				_movementDirection.y = 0;
			} else if (_direction == Direction.WEST)
			{
				_movementDirection.x = Time.deltaTime * _wallSpeed;
				_movementDirection.y = 0;
			} else if (_direction == Direction.SOUTH)
			{
				_movementDirection.x = 0;
				_movementDirection.y = Time.deltaTime * _wallSpeed;
			} else {
				_movementDirection.x = 0;
				_movementDirection.y = -Time.deltaTime * _wallSpeed;
			}

			transform.position += _movementDirection;
		}

	}

	public void ChangeColor(bool entered)
	{
		if (entered)
		{
			_enteredWall = entered;
		}
		_spriteRenderer.color = (entered) ? new Color(0, 1, 0, 1) : new Color(1, 1, 1, 1);
	}



}

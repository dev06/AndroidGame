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

	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_wallObjects = GameObject.FindWithTag("WallObjects");
		_spriteRenderer = GetComponent<SpriteRenderer>();
		transform.position += offsetedPosition;
	}


	void Update ()
	{
		Move(_gameController.facingDirection);
	}


	private void Move(Direction _direction)
	{
		float _wallSpeed = Constants.WallSpeed;
		if (move)
		{
			if (_direction == Direction.EAST)
			{
				transform.position += new Vector3(-Time.deltaTime * _wallSpeed, 0 , 0);
			} else if (_direction == Direction.WEST)
			{
				transform.position += new Vector3(Time.deltaTime * _wallSpeed, 0 , 0);
			} else if (_direction == Direction.SOUTH)
			{
				transform.position += new Vector3(0, Time.deltaTime * _wallSpeed , 0);
			} else {
				transform.position += new Vector3(0, -Time.deltaTime * _wallSpeed, 0);
			}
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


	public void Hit(bool b)
	{
		_shouldDestroy = b;
	}
}

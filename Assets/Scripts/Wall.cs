using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization

	public GameObject previousWall;
	private bool _wallPassed;
	private GameController _gameController;
	private GameObject _wallObjects;
	private SpriteRenderer _spriteRenderer;
	public bool move;
	private bool _enteredWall;
	private bool _shouldDestroy;
	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

		_wallObjects = GameObject.FindWithTag("WallObjects");
		_spriteRenderer = GetComponent<SpriteRenderer>();

	}


	void Update ()
	{

		Move(GameController.direction);


		Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

		if (_shouldDestroy)
		{
			if (screenPoint.y < 0)
				Destroy(gameObject, 2);
		}
	}


	private void Move(float direction)
	{
		if (move)
		{
			if (direction == .75f)
			{
				transform.position += new Vector3(-Time.deltaTime * _gameController.speed, 0 , 0);
			} else if (direction == .25)
			{
				transform.position += new Vector3(Time.deltaTime * _gameController.speed, 0 , 0);
			} else if (direction == .5f)
			{
				transform.position += new Vector3(0, Time.deltaTime * _gameController.speed , 0);
			} else {
				transform.position += new Vector3(0, -Time.deltaTime * _gameController.speed , 0);
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

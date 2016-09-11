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
	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		if (previousWall != null)
		{
			float _pixelToUnit = .32f;
			transform.position = previousWall.transform.GetChild(0).transform.position;
			Transform _previousWall = previousWall.transform;
			if ( transform.rotation.eulerAngles.z != 0)
			{
				transform.position = new Vector3( transform.position.x,  transform.position.y + _previousWall.transform.localScale.x / 2.0f * _pixelToUnit);

				if ( transform.rotation.eulerAngles.z > 180)
				{
					transform.position = new Vector3( transform.position.x -  transform.localScale.x / 2.0f * _pixelToUnit,  transform.position.y -  transform.localScale.x / 2.0f * _pixelToUnit);
				} else
				{
					transform.position = new Vector3( transform.position.x +  transform.localScale.x / 2.0f * _pixelToUnit,  transform.position.y -  transform.localScale.x / 2.0f * _pixelToUnit);
				}
			} else
			{
				transform.position = new Vector3( transform.position.x ,  transform.position.y -  transform.localScale.x / 2.0f * _pixelToUnit);
			}


		}
		_wallObjects = GameObject.FindWithTag("WallObjects");
		_spriteRenderer = GetComponent<SpriteRenderer>();

	}


	void Update ()
	{

		if (move)
			transform.position -= new Vector3(0, Time.deltaTime * 25f , 0);

		Vector3 screenPoint = Camera.main.WorldToViewportPoint (transform.position + transform.localScale * .32f);


		if (screenPoint.y < 0)
		{

			if (_enteredWall)
			{
				Destroy(gameObject);
				_gameController.GenerateEmptyGameObjects(1);
			}


			//_gameController.ModifyTransformForObjects(gameObject, _wallObjects.transform.GetChild(_wallObjects.transform.childCount - 1).gameObject, Modifier.TRANSFORM);
		}
	}




	void OnCollisionStay2D(Collision2D  col)
	{

	}
	void OnCollisionExit2D(Collision2D  col)
	{

	}

	void OnTriggerStay2D(Collider2D col)
	{

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

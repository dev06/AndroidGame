using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	private SpriteRenderer _sprite;
	private GameObject _enterObject, _exitObject;
	private GameObject _currentWall;
	private int _hits;
	private GameController _gameController;
	void Start () {
		_sprite = GetComponent<SpriteRenderer>();
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	void Update ()
	{

		if (_hits <= 0)
		{
			_sprite.color = new Color(1, 0, 0, 1);
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
		}

		Debug.Log(_hits);

		// if (_currentWall != null)
		// {
		// 	if (CheckIfOutsideBounds(_currentWall))
		// 	{
		// 		_sprite.color = new Color(1, 0, 0, 1);
		// 	} else {
		// 		_sprite.color = new Color(0, 1, 1, 1);
		// 	}
		// }

	}


	void OnTriggerEnter2D(Collider2D col)
	{
		_currentWall = col.gameObject;
		_sprite.color = new Color(0, 1, 1, 1);
		_hits++;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//_exitObject = col.gameObject;
		col.gameObject.SendMessage("ChangeColor", false);
		col.gameObject.SendMessage("Hit", true);
		_hits--;
		_gameController.GenerateEmptyGameObjects(1);
	}

	void OnTriggerStay2D(Collider2D col)
	{
		//_currentWall = col.gameObject;
		//_enterObject = col.gameObject;
		col.gameObject.SendMessage("ChangeColor", true);
		//_sprite.color = new Color(0, 1, 1, 1);

	}

	private bool CheckIfOutsideBounds(GameObject _currentWall)
	{
		bool _outside = false;
		float px = transform.position.x;
		float py = transform.position.y;
		float psX = transform.localScale.x * .32f;
		float psY = transform.localScale.y * .32f;

		float wx = _currentWall.transform.position.x;
		float wy = _currentWall.transform.position.y;
		float wsX = _currentWall.transform.localScale.x * .32f;
		float wsY = _currentWall.transform.localScale.y * .32f ;

		float _rotation = _currentWall.transform.eulerAngles.z;

		if (_rotation == 0f || _rotation == 180f)
		{
			if (px + psX < (wx - wsX) || px - psX > (wx + wsX) || py + psY < wy)
				_outside = true;
		} else {
			if (_rotation == 270f)
			{
				if (py - psY > wy || px + psX < wx)
				{
					_outside = true;
				}
			} else if (_rotation == 90f)
			{
				if (py - psY > wy)
				{
					_outside = true;
				}
			}
		}






		return _outside;
	}




}

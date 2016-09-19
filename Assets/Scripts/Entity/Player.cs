using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	private GameController _gameController;
	private SpriteRenderer _sprite;
	private GameObject _enterObject, _exitObject;
	private GameObject _currentWall;
	private int _hits;

	void Start ()
	{
		_sprite = GetComponent<SpriteRenderer>();
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	void Update ()
	{
		if (_currentWall != null)
		{
			if (_hits <= 0)
			{
				_sprite.color = new Color(1, 0, 0, 1);
				UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
			}
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Walls")
		{
			_sprite.color = new Color(0, 1, 1, 1);
			_hits++;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		_exitObject = col.gameObject;
		_hits--;
		_gameController.gameObject.SendMessage("DestroyWall", col.gameObject);
		_gameController.poolManager.PoolObject(col.gameObject, _gameController.wallObjects.transform.GetChild(_gameController.wallObjects.transform.childCount - 1).gameObject);
	}

	void OnTriggerStay2D(Collider2D col)
	{
		_currentWall = col.gameObject;
	}
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Quaternion targetRotation;
	public float zRotation;

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
				//UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
			}
		}

		Vector3 _playerRotation = transform.eulerAngles;
		_playerRotation = Vector3.Lerp(_playerRotation, new Vector3(0, 0, zRotation), Time.deltaTime / .05f);
		transform.rotation = Quaternion.Euler(_playerRotation);
		Debug.Log(zRotation);
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
		_gameController.poolManager.PoolObject(col.gameObject, _gameController.wallObjects.transform.GetChild(_gameController.wallObjects.transform.childCount - 1).gameObject);
	}

	void OnTriggerStay2D(Collider2D col)
	{
		_currentWall = col.gameObject;
	}
	private bool IsNaN(Quaternion q) {
		return float.IsNaN(q.x) || float.IsNaN(q.y) || float.IsNaN(q.z) || float.IsNaN(q.w);
	}
}

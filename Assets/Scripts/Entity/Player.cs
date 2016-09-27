using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public float zRotation;

	private float _zRotationAmp;
	private float _zRotationFreq = .6f;
	private float _bob;
	private GameController _gameController;
	private SpriteRenderer _sprite;
	private GameObject _enterObject, _exitObject;
	private GameObject _currentWall;
	private int _hits;
	private bool _swiped;
	private float bb;
	private float vel;

	void Start ()
	{
		_sprite = GetComponent<SpriteRenderer>();
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		EventManager.OnSwipe += SetAmp;
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


		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, zRotation), Time.deltaTime * 20.4f);
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


	public void SetAmp()
	{
		_zRotationAmp = 10.0f;
		_bob = 0;
		bb = 0;
	}
}

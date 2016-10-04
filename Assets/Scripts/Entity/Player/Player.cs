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
	private Transform _particleEffect;
	private int _hits;
	private bool _swiped;
	private float bb;
	private float vel;

	void Start ()
	{
		_sprite = GetComponent<SpriteRenderer>();
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_particleEffect = transform.FindChild("ParticleEffect");
		EventManager.OnSwipe += SetAmp;
		_hits = 0;
	}

	void Update ()
	{

		if (_gameController.gameState == GameState.GAME)
		{
			if (_currentWall != null)
			{
				if (_hits <= 0)
				{
					_sprite.color = new Color(1, 0, 0, 1);
					if (_gameController.endGameUponDeath)
					{
						UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
					}
				}
				if (_gameController.facingDirection == Direction.NORTH || _gameController.facingDirection == Direction.SOUTH) {
					_particleEffect.position = new Vector3(_currentWall.transform.position.x, 0 , 0);
				} else {
					_particleEffect.position = new Vector3(0, _currentWall.transform.position.y, 0);
				}
			}
		}
		if (_gameController.autoPlay == false)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, zRotation), Time.deltaTime * 20.4f);
		} else {
			if (_gameController.wallObjects.transform.childCount > 0)
			{

			}
		}

		if (_gameController.autoPlay)
		{
			if (start)
			{
				timer += Time.deltaTime;
			}

			if (timer > .012f)
			{
				if (colll.gameObject  != null)
				{
					Turn(colll);
					timer = 0;
					start = false;
				}
			}

			if (index < 0)
			{
				index = Constants.directions.Length - 1;
			} else if (index > Constants.directions.Length - 1)
			{
				index = 0;
			}

			_gameController.facingDirection = Constants.directions[index];
		}

	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Path")
		{
			//_sprite.color = new Color(0, 1, 1, 1);
			_hits++;

		}


		start = true;
		colll = col;

		//	Debug.Log(_gameController.facingDirection);


	}
	int index;
	bool start;
	float timer;
	Collider2D colll;

	void Turn(Collider2D col)
	{
		Transform _nextWall = _gameController.wallObjects.transform.GetChild(1).transform;
		if (_nextWall.gameObject == col.gameObject)
		{
			transform.rotation = _nextWall.transform.rotation;
			float _nextRot = _nextWall.eulerAngles.z;
			float _currRot = _currentWall.transform.eulerAngles.z;

			if (_currRot == 0)
			{
				if (_nextRot == 90)
				{
					index--;
				} else if (_nextRot == 270)
				{
					index++;
				}
			} else if (_currRot == 90)
			{
				if (_nextRot == 180)
				{
					index--;
				} else if (_nextRot == 0)
				{
					index++;
				}
			}
			else if (_currRot == 270)
			{
				if (_nextRot == 180)
				{
					index++;
				} else if (_nextRot == 0)
				{
					index--;
				}
			} else if (_currRot == 180)
			{
				if (_nextRot == 270)
				{
					index--;
				} else if (_nextRot == 90)
				{
					index++;
				}
			}
		}
	}


	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Path")
		{
			_exitObject = col.gameObject;
			_hits--;
			if (_exitObject == _gameController.wallObjects.transform.GetChild(0).gameObject)
			{
				_gameController.poolManager.PoolObject(col.gameObject, _gameController.wallObjects.transform.GetChild(_gameController.wallObjects.transform.childCount - 1).gameObject);

			}

		}

	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Path")
		{
			_currentWall = col.gameObject;
		}
	}

	public void SetAmp()
	{
		_zRotationAmp = 10.0f;
		_bob = 0;
		bb = 0;
	}
}

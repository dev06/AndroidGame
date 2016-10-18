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
	private Transform _center;
	private int _hits;
	private bool _swiped;
	private float vel;
	private float _deathThresHoldTimer;
	private bool _startDeathTimer;


	void Start ()
	{
		_sprite = GetComponent<SpriteRenderer>();
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_particleEffect = transform.FindChild("ParticleEffect");
		_center = transform.FindChild("Center");
		EventManager.OnSwipe += SetAmp;
		_hits = 0;
	}

	void Update ()
	{
		CheckIfDead();

		if (_gameController.autoPlay == false)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, zRotation), Time.deltaTime * 20.4f);
		}

	}

	private void CheckIfDead()
	{
		if (_gameController.gameState == GameState.GAME)
		{
			if (_currentWall != null)
			{
				if (_hits <= 0)
				{

					if (_gameController.endGameUponDeath)
					{

						_startDeathTimer = true;

					}
				}

				if (_gameController.dead == false)
				{
					if (_gameController.facingDirection == Direction.NORTH || _gameController.facingDirection == Direction.SOUTH) {
						_particleEffect.position = new Vector3(_currentWall.transform.position.x, 0 , 0);
						_center.position = new Vector3(_currentWall.transform.position.x, 0, 0);
					} else {
						_particleEffect.position = new Vector3(0, _currentWall.transform.position.y, 0);
						_center.position = new Vector3(0, _currentWall.transform.position.y, 0);
					}
				} else
				{
					_particleEffect.gameObject.SetActive(false);
				}
			}
		}


		if (_startDeathTimer)
		{
			_deathThresHoldTimer += Time.deltaTime;
		}

		if (_deathThresHoldTimer > .1f)
		{
			_sprite.color = new Color(1, 0, 0, 1);
			Die();
			_startDeathTimer = false;
			_deathThresHoldTimer = 0;
		}
	}

	private void Die()
	{
		if (EventManager.OnDeath != null)
		{
			EventManager.OnDeath();

		}

		_gameController.dead = true;
		StartCoroutine("Restart");
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Path")
		{
			_hits++;
			_startDeathTimer = false;
			_deathThresHoldTimer = 0;
		}
	}


	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Path")
		{
			_exitObject = col.gameObject;
			_hits--;

			if (_gameController.poolManager.pathReserve == null)
			{
				if (_exitObject == _gameController.wallObjects.transform.GetChild(0).gameObject)
				{
					_gameController.poolManager.pathReserve = col.gameObject;
				}
			} else
			{
				if (_exitObject == _gameController.wallObjects.transform.GetChild(1).gameObject)
				{
					_gameController.poolManager.PoolPath(_gameController.poolManager.pathReserve, _gameController.wallObjects.transform.GetChild(_gameController.wallObjects.transform.childCount - 1).gameObject);
					_gameController.poolManager.pathReserve = col.gameObject;
				}
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
	}

	IEnumerator Restart()
	{
		yield return new WaitForSeconds(2);
		UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");

	}
}

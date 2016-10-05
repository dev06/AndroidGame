using UnityEngine;
using System.Collections;

public class AutoPlay : MonoBehaviour {

	public AutoPlayID autoPlayID;
	private GameController _gameController;
	private Player _player;
	private static int _directionIndex;
	private Transform _particleEffect;
	private GameObject _currentObject;

	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_player = transform.parent.GetComponent<Player>();
		_particleEffect = _player.gameObject.transform.FindChild("ParticleEffect");
	}

	void Update ()
	{

		if (_gameController.dead == false)
		{
			if (_currentObject != null)
			{
				if (_gameController.facingDirection == Direction.NORTH || _gameController.facingDirection == Direction.SOUTH) {
					_particleEffect.position = new Vector3(_currentObject.transform.position.x, 0 , 0);
				} else {
					_particleEffect.position = new Vector3(0, _currentObject.transform.position.y, 0);
				}
			}

			_particleEffect.GetChild(0).gameObject.SetActive(_currentObject != null);

		} else
		{
			_particleEffect.gameObject.SetActive(false);
		}

	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (autoPlayID == AutoPlayID.Colliders)
		{
			if (col.gameObject.tag == "Path")
			{
				_currentObject = col.gameObject;

			}
		}
	}

	void OnDisable()
	{
		_directionIndex = 0;
	}


}

public enum AutoPlayID
{
	Indicators,
	Colliders,
}

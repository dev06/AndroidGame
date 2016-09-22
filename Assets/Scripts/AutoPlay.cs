using UnityEngine;
using System.Collections;

public class AutoPlay : MonoBehaviour {

	GameController _gameController;
	Quaternion _rotation;

	void Start () {
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	private void ChangeDirections(Direction direction)
	{





	}



	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject != transform.parent.gameObject)
		{
			if (_gameController.autoPlay)
			{
				ChangeDirections(_gameController.facingDirection);
			}
		}
	}


}

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
		if (direction == Direction.NORTH)
		{
			if (gameObject.name == "Right")
			{
				_rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
			} else if (gameObject.name == "Left")
			{
				_rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
			}
		} else if (direction == Direction.SOUTH) {
			if (gameObject.name == "Right")
			{
				_rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
			} else if (gameObject.name == "Left")
			{
				_rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
			}

		}
		else
		{
			if (direction == Direction.EAST)
			{
				if (gameObject.name == "Top") {
					_rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
				} else if (gameObject.name == "Bottom") {
					_rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
				}
			} else {
				if (gameObject.name == "Top") {
					_rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
				} else if (gameObject.name == "Bottom") {
					_rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
				}
			}
		}


		_gameController.player.transform.rotation = _rotation;

	}



	void OnTriggerEnter2D(Collider2D col)
	{
		if (_gameController.autoPlay)
		{
			ChangeDirections(_gameController.facingDirection);
		}
	}


}

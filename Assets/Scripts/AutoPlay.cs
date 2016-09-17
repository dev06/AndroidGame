using UnityEngine;
using System.Collections;

public class AutoPlay : MonoBehaviour {

	GameController _gameController;
	void Start () {
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}


	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (_gameController.autoPlay)
		{
			if (GameController.direction == 0)
			{
				if (gameObject.name == "Right")
				{
					Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
				} else if (gameObject.name == "Left")
				{
					Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
				}
			} else if (GameController.direction == .5f) {
				if (gameObject.name == "Right")
				{
					Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
				} else if (gameObject.name == "Left")
				{
					Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
				}

			}
			else
			{
				if (GameController.direction == .75f)
				{
					if (gameObject.name == "Top") {
						Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
					} else if (gameObject.name == "Bottom") {
						Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
					}
				} else {
					if (gameObject.name == "Top") {
						Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
					} else if (gameObject.name == "Bottom") {
						Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
					}
				}
			}
		}

	}
}

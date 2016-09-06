using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization
	private Vector2 _pointerDown;
	private Vector2 _pointerUp;
	private GameObject _character;
	private int _direction;
	private int _threshold = 150;
	private GameObject _ui;
	void Start ()
	{
		_character = GameObject.Find("Character");
		_ui = (GameObject)(Resources.Load("Prefabs/Image"));

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_pointerDown = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_pointerUp = Input.mousePosition;
		}


		if (Mathf.Abs(_pointerDown.x - _pointerUp.x) > _threshold)
		{
			if (_pointerUp != Vector2.zero)
			{



				if (_pointerDown.x - _pointerUp.x >= 0)
				{
					_direction = -1;


				} else if (_pointerDown.x - _pointerUp.x < 0) {

					_direction = 1;

				}
				_character.transform.position = Vector2.Lerp(_character.transform.position, new Vector2(_direction, 0), Time.deltaTime * 15f);
			}


		}




	}
}

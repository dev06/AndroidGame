using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization
	private Vector2 _pointerDown;
	private Vector2 _pointerUp;
	private GameObject _character;
	private float _direction;
	private int _threshold = 150;
	private GameObject _ui;
	private RectTransform _canvasRect;
	void Start ()
	{
		_character = GameObject.Find("Character");
		_ui = (GameObject)(Resources.Load("Prefabs/Image"));
		_canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

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
			Calculate();
		}



		_character.transform.position = Vector2.Lerp(_character.transform.position, new Vector2(_direction, 0), Time.deltaTime * 15f);


	}

	private void Calculate()
	{
		if (Mathf.Abs(_pointerDown.x - _pointerUp.x) > _threshold)
		{


			if (_pointerDown.x - _pointerUp.x >= 0)
			{
				_direction -= .5f;

			} else if (_pointerDown.x - _pointerUp.x < 0) {

				_direction += .5f;

			}

		}
	}
}

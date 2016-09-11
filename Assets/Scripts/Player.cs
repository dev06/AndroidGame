using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization

	SpriteRenderer _sprite;
	GameObject _currentWall;
	void Start () {
		_sprite = GetComponent<SpriteRenderer>();
		_controller = GameObject.Find("GameController").GetComponent<GameController>();
	}

	float _timer;
	float _outOfboundsTimer;
	bool _entered;
	bool _outSide;

	GameController _controller;
	GameObject _enterObject, _exitObject;

	void Update ()
	{


		if (_enterObject != null && _exitObject != null)
			if (_enterObject.name == _exitObject.name) {
				_sprite.color = new Color(1, 0, 0, 1);
				//_controller.DestroyMap();
			}



	}


	void OnTriggerEnter2D(Collider2D col)
	{





	}


	void OnTriggerExit2D(Collider2D col)
	{

		_exitObject = col.gameObject;



	}

	void OnTriggerStay2D(Collider2D col)
	{
		_enterObject = col.gameObject;
		_currentWall = col.gameObject;
		_sprite.color = new Color(0, 1, 1, 1);

	}




}

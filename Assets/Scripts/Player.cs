using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization

	SpriteRenderer _sprite;
	GameObject _enterObject, _exitObject;

	void Start () {
		_sprite = GetComponent<SpriteRenderer>();

	}



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
		_sprite.color = new Color(0, 1, 1, 1);

	}




}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	private SpriteRenderer _sprite;
	private GameObject _enterObject, _exitObject;
	private int _hits;

	void Start () {
		_sprite = GetComponent<SpriteRenderer>();

	}



	void Update ()
	{

		if (_exitObject != null)
		{
			if (_hits <= 0)
			{
				_sprite.color = new Color(1, 0, 0, 1);
				UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
			}
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		_hits++;
	}


	void OnTriggerExit2D(Collider2D col)
	{
		_exitObject = col.gameObject;
		col.gameObject.SendMessage("ChangeColor", false);
		_hits--;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		_enterObject = col.gameObject;
		col.gameObject.SendMessage("ChangeColor", true);
		_sprite.color = new Color(0, 1, 1, 1);

	}




}

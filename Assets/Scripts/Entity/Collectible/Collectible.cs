using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	protected GameController _gameController;

	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	void Update ()
	{

	}
}

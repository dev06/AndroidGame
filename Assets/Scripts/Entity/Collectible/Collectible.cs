using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	protected GameController _gameController;

	void Start ()
	{
		Init();
	}

	protected void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	void Update ()
	{

	}
}

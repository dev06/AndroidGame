using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	protected GameController _gameController;

	public float life;
	public bool shouldDestroy;

	void Start ()
	{
		Init();
	}

	protected void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

}

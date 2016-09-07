using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	static bool dead;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		dead = false;
	}

	void OnCollisionStay2D(Collision2D col)
	{
		dead = false;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		dead = true;
	}
}

using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		transform.position -= new Vector3(0, Time.deltaTime * .1f, 0);
	}


	void OnCollisionExit2D(Collision2D  col)
	{

	}
}

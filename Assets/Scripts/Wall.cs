using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	float offset = 0;
	float yOffset = 0;
	void Update ()
	{
		float num = .005f;
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			offset -= num;
		} else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			offset += num;
		} else {
			offset = 0;
		}



		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			yOffset -= num;
		} else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			yOffset += num;
		} else {
			yOffset = 0;
		}

		transform.position -= new Vector3(offset, Time.deltaTime * .5f , 0);


	}


	void OnCollisionStay2D(Collision2D  col)
	{

	}
	void OnCollisionExit2D(Collision2D  col)
	{

	}

	void OnTriggerStay2D(Collider2D col)
	{

	}



}

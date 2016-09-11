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

		transform.position -= new Vector3(offset, Time.deltaTime * .4f , 0);

		Vector3 screenPoint = Camera.main.WorldToViewportPoint (transform.position + transform.localScale * .32f);
		// if (screenPoint.y < 0) {
		// 	Destroy(gameObject);
		// }
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

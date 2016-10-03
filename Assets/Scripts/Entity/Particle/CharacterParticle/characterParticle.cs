using UnityEngine;
using System.Collections;

public class characterParticle : Particle {

	private SpriteRenderer _sr;
	void Start ()
	{
		Init();
		_sr = GetComponent<SpriteRenderer>();
		_sr.color = new Color(1, 1, 1, 0);
	}

	void Update()
	{

	}

	// void OnTriggerEnter2D(Collider2D col)
	// {
	// 	if (col.gameObject.tag == "Path")
	// 	{
	// 		_sr.color = new Color(1, 1, 1, 1);
	// 	}
	// }

	// void OnTriggerExit2D(Collider2D col)
	// {
	// 	if (col.gameObject.tag == "Path")
	// 	{
	// 		_sr.color = new Color(1, 1, 1, 0);
	// 	}

	// }

}

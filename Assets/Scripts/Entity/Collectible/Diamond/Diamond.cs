using UnityEngine;
using System.Collections;

public class Diamond : Collectible {

	// Use this for initialization
	void Start ()
	{
		Init();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Entity/Player")
		{
			base.SetActive(false);
		}
	}

	public override void SetActive(bool _value)
	{
		base.SetActive(_value);
	}

	public override void OnModifyTransform()
	{
		SetActive(true);
	}
}

using UnityEngine;
using System.Collections;

public class Diamond : Collectible {

	void Start ()
	{
		Init();
	}

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
		if (_parentObject != GameController.Instance.wallObjects.transform.GetChild(0).gameObject)
		{
			SetActive(true);
		}
	}
}

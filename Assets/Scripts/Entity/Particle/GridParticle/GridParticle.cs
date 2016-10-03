using UnityEngine;
using System.Collections;

public class GridParticle : Particle {

	private Vector3 _velocity;

	void Start () {
		Init();
		_velocity = new Vector3(0, Constants.GridParticleVelocity, 0);
	}

	void Update ()
	{

		if (shouldDestroy)
		{
			Destroy(gameObject, life);
		}

		transform.position += _velocity * Time.deltaTime;
		Reset();
	}

	void Reset()
	{
		if (transform.position.y < -15)
		{
			transform.position = new Vector3(0, 15, 0);
		}
	}
}

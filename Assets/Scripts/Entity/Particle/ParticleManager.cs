using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

	private GameObject _particleList;

	void Start ()
	{
		_particleList = GameObject.FindWithTag("Particles");
		SpawnParticle(GameResources.GridParticle_resource, new Vector3(0, -5, 0));
		SpawnParticle(GameResources.GridParticle_resource, new Vector3(0, 5, 0));

	}

	private void SpawnParticle(GameObject _resource, Vector3 _position)
	{
		GameObject _particle = (GameObject)Instantiate(_resource, _position, Quaternion.identity);
		_particle.transform.parent = _particleList.transform;
	}

	void Update () {

	}
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameController _gameController;
	private Vector3 _jitterVel;
	private Vector3 _jitterPos;
	private float _jitterPosVelX;
	private float _jitterPosVelY;
	private Vector3 _camInitLocalPos;
	private float _jitterRadius;
	private float _jitterRadVel;
	private bool _onDeath;
	void Start () {

		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_jitterVel = Vector3.zero;
		_camInitLocalPos = transform.localPosition;
		_jitterRadius = 0.035f;
		EventManager.OnDeath += OnPlayerDeath;
	}


	void Update ()
	{
		JitterCamera(_jitterRadius);
		if (_onDeath)
		{
			_jitterRadius = Mathf.SmoothDamp(_jitterRadius, 0f, ref _jitterRadVel, .5f);
		}
	}

	private void JitterCamera(float intensity)
	{
		_jitterVel.x = Random.Range(-intensity, intensity);
		_jitterVel.y = Random.Range(-intensity, intensity);
		_jitterPos.x = Mathf.SmoothDamp(_jitterPos.x, _jitterVel.x, ref _jitterPosVelX, .01f);
		_jitterPos.y = Mathf.SmoothDamp(_jitterPos.y, _jitterVel.y, ref _jitterPosVelY, .01f);
		_jitterPos.z = -2;
		transform.localPosition = _camInitLocalPos + _jitterPos;
	}

	private void OnPlayerDeath()
	{
		_onDeath = true;
	}
}

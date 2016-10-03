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

	void Start () {

		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_jitterVel = Vector3.zero;
		_camInitLocalPos = transform.localPosition;
		_jitterRadius = 0.05f;
	}


	void Update ()
	{
		JitterCamera(0.5f);
	}

	private void JitterCamera(float intensity)
	{
		_jitterVel.x = Random.Range(-_jitterRadius, _jitterRadius);
		_jitterVel.y = Random.Range(-_jitterRadius, _jitterRadius);
		_jitterPos.x = Mathf.SmoothDamp(_jitterPos.x, _jitterVel.x, ref _jitterPosVelX, .01f);
		_jitterPos.y = Mathf.SmoothDamp(_jitterPos.y, _jitterVel.y, ref _jitterPosVelY, .01f);
		_jitterPos.z = -2;
		transform.localPosition = _camInitLocalPos + _jitterPos;
	}


}

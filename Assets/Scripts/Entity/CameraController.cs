using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public bool startLerping;
	public Quaternion targetRotation;
	private GameController _gameController;
	private Quaternion _playerRotation;
	void Start () {

		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_playerRotation = _gameController.transform.rotation;
	}

	// Update is called once per frame
	void Update ()
	{
		if (startLerping)
		{
			LerpCamera(targetRotation);
		}
	}

	private void LerpCamera(Quaternion targetRotation)
	{
		_playerRotation = Quaternion.Lerp(_playerRotation, targetRotation, Time.deltaTime * 5.0f);
		_gameController.player.transform.rotation = _playerRotation;
	}
}

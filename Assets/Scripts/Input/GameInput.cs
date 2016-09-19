using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour {

	public GameController gameController;
	private Vector2 _pointerDown;
	private Vector2 _pointerUp;


	private void CalculateSwipe()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
		if (_abs > Constants.SwipeThresHold)
		{
			float _difference = -(_pointerDown.x - _pointerUp.x);
			float zRotation = (_difference < 0) ? 90 : -90;
			Camera.main.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, zRotation));
		}
		_pointerDown = Vector2.zero;
		_pointerUp = Vector2.zero;
	}


	public void RegisterSwipe()
	{
		if (gameController.autoPlay == false)
		{
			if (Input.GetMouseButtonDown(0))
			{
				_pointerDown = Input.mousePosition;
			}

			if (Input.GetMouseButtonUp(0))
			{
				_pointerUp = Input.mousePosition;
				CalculateSwipe();
			}
		}
	}
}

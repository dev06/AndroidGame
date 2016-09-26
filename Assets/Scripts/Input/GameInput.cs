﻿using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour {

	public GameController gameController;
	private Vector2 _pointerDown;
	private Vector2 _pointerUp;
	private GameController _gameController;
	private Vector3 _rotation;
	private Player _player;

	void Start()
	{
		_gameController = GetComponent<GameController>();
		_player = _gameController.player.GetComponent <Player>();
		_rotation = Vector3.zero;
	}

	private void CalculateSwipe()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
		if (_abs > Constants.SwipeThresHold)
		{
			float _difference = -(_pointerDown.x - _pointerUp.x);
			float zRotation = (_difference < 0) ? 90 : -90;

			_player.targetRotation *= Quaternion.Euler(new Vector3(0, 0, zRotation));

			_player.zRotation += zRotation;
			_player.zRotation %= 360f;

			float _rotationZ = _gameController.player.transform.eulerAngles.z;
			if (_rotationZ == 90)
			{
				_gameController.facingDirection = Direction.WEST;
			} else if (_rotationZ == 270)
			{
				_gameController.facingDirection = Direction.EAST;
			} else if (_rotationZ == 0)
			{
				_gameController.facingDirection = Direction.NORTH;
			} else if (_rotationZ == 180)
			{
				_gameController.facingDirection = Direction.SOUTH;
			}
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

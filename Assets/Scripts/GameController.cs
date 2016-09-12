﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization

	public static float direction;

	public float _rotationFreq;
	public float _size;
	public float _minHeight;
	public float _maxHeight;


	private Vector2 _pointerDown;
	private Vector2 _pointerUp;

	private int _threshold = 20;
	private GameObject _wall;
	private GameObject _wallObjects;
	private GameObject _player;
	private List<GameObject> walls;

	private float _pixelToUnit = .32f;

	void Awake ()
	{
		_wall = Resources.Load("Prefabs/Wall") as GameObject;
		_wallObjects = GameObject.FindWithTag("WallObjects");
		_player = GameObject.FindWithTag("Entity/Player");
		walls = new List<GameObject>();
		GenerateEmptyGameObjects(2);
	}

	// Update is called once per frame

	void Update ()
	{
		DestroyMap();
		RegisterSwipe();
		float rotation = _wallObjects.transform.rotation.eulerAngles.z;
		direction = rotation / 360.0f;
	}


	private void Calculate()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
		if (_abs > _threshold)
		{
			float _difference = -(_pointerDown.x - _pointerUp.x);
			float zRotation = (_difference < 0) ? -90 : 90;
			_wallObjects.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, zRotation));

		}

		_pointerDown = Vector2.zero;
		_pointerUp = Vector2.zero;
	}



	private void GenerateWallMap()
	{


		for (int i = 0; i < 4; i++)
		{
			GameObject _currentWall = null;
			GameObject _lastwall = null;
			if (_wallObjects.transform.childCount > 0)
			{
				_lastwall = _wallObjects.transform.GetChild(_wallObjects.transform.childCount - 1).gameObject;
			}


			_currentWall = Instantiate(_wall, new Vector2(0, 0 ), Quaternion.identity) as GameObject;
			_currentWall.name = "Wall" + _currentWall.GetHashCode();
			_currentWall.transform.parent = _wallObjects.transform;

			float _height = Random.Range(_minHeight, _maxHeight);
			bool _rotated = false;

			if (_lastwall != null)
			{
				if (Random.Range(0.0f, 1.1f) < _rotationFreq)
				{
					if (_lastwall.transform.rotation.eulerAngles.z != 0)
					{
						Quaternion _rotation = _lastwall.transform.rotation;
						_currentWall.transform.rotation = _rotation;
						_rotated = true;
					} else {
						float _rotation = (Random.Range(0, 2) == 0) ? 90 : -90;
						_currentWall.transform.Rotate(new Vector3(0, 0, _rotation));
						_rotated = true;
					}

				}

			}


			_currentWall.transform.localScale = new Vector2(_size, _height);

			walls.Add(_currentWall);
			//PositionWall(_currentWall, _lastwall, _rotated);
		}
	}


	public void GenerateEmptyGameObjects(int value)
	{
		for (int i = 0; i < value; i++)
		{
			_wallObjects.transform.position = _player.transform.position;
			GameObject _clone = Instantiate(_wall, Vector2.zero, Quaternion.identity) as GameObject;
			_clone.name = "Wall" + _clone.GetHashCode();
			_clone.tag = "Walls";
			_clone.transform.parent = _wallObjects.transform;
			walls.Add(_clone);
			GameObject _lastClone = (_wallObjects.transform.childCount > 1) ? _wallObjects.transform.GetChild(_wallObjects.transform.childCount - 2).gameObject : null;
			_clone.GetComponent<Wall>().previousWall = (_lastClone != null ) ? _lastClone : null;
			ModifyTransformForObjects(_clone, _lastClone, Modifier.TRANSFORM);
		}

	}


	public void ModifyTransformForObjects(GameObject _object, GameObject _previousObject, Modifier _modifier)
	{
		float _height = Random.Range(_minHeight, _maxHeight);
		float _rotationFreqNumber = Random.Range(0.0f, 1.1f);
		float _rotationDirection = (Random.Range(0, 2) == 0) ? 90 : 270;

		if (_modifier == Modifier.TRANSFORM) // USE THIS FOR MODIFING ENTIRE TRANSFORM
		{
			_object.transform.localScale = new Vector2(_size, _height);

			if (_previousObject != null)
			{

				float _previousObjectRotation = _wallObjects.transform.rotation.eulerAngles.z + _previousObject.transform.localRotation.eulerAngles.z;
				if (_rotationFreqNumber < _rotationFreq)
				{
					Debug.Log(_previousObject + " " + _previousObject.transform.localRotation.eulerAngles.z + " " + _previousObject.transform.rotation.eulerAngles.z);

					if (_previousObjectRotation == 0)
					{
						_object.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _rotationDirection));
					} else {
						_object.transform.rotation = _previousObject.transform.rotation;
					}
				}
				_object.transform.position = _previousObject.transform.GetChild(0).transform.position;
				PositionWall(_object, _previousObject);
			}
		}
	}



	public void PositionWall(GameObject _currentWall, GameObject _lastWallTransform)
	{

		Transform _previousWall = _lastWallTransform.transform;
		if (_currentWall.transform.rotation.eulerAngles.z != 0)
		{
			_currentWall.transform.position = new Vector3(_currentWall.transform.position.x, _currentWall.transform.position.y + _previousWall.transform.localScale.x / 2.0f * _pixelToUnit);

			if (_currentWall.transform.rotation.eulerAngles.z > 180)
			{
				_currentWall.transform.position = new Vector3(_currentWall.transform.position.x - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit, _currentWall.transform.position.y - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit);
			} else
			{
				_currentWall.transform.position = new Vector3(_currentWall.transform.position.x + _currentWall.transform.localScale.x / 2.0f * _pixelToUnit, _currentWall.transform.position.y - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit);
			}
		} else
		{
			_currentWall.transform.position = new Vector3(_currentWall.transform.position.x , _currentWall.transform.position.y - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit);
		}


	}


	private void RegisterSwipe()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_pointerDown = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_pointerUp = Input.mousePosition;
			Calculate();
		}
	}


	public void DestroyMap()
	{

		if (Input.GetMouseButtonDown(1))
		{

			// if (walls.Count > 0)
			// {
			// 	GameObject[] _walls = GameObject.FindGameObjectsWithTag("Walls");
			// 	for (int i = 0; i < _walls.Length; i++)
			// 	{
			// 		//Destroy(_walls[i]);
			// 	}

			// 	//walls.Clear();

			// 	//GameObject.Find("WallObjects").transform.rotation = Quaternion.Euler(Vector3.zero);
			// }

			GenerateEmptyGameObjects(1);
		}
	}


	public void Reset()
	{
		if (walls.Count > 0)
		{
			GameObject[] _walls = GameObject.FindGameObjectsWithTag("Walls");
			for (int i = 0; i < _walls.Length; i++)
			{
				Destroy(_walls[i]);
			}

			walls.Clear();

			_wallObjects.transform.rotation = Quaternion.Euler(Vector3.zero);
		}
	}

}


public enum Modifier
{
	TRANSFORM,
	POSITION,
	ROTATION,
	SCALE,
}

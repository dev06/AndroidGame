using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization

	public static float direction;
	public float speed;

	public float _rotationFreq;
	public float _size;
	public float _minHeight;
	public float _maxHeight;
	public bool  generateOnTimer;
	public Swipe swipe;
	public float  rate;

	private Vector2 _pointerDown;
	private Vector2 _pointerUp;

	private int _threshold = 20;
	private GameObject _wall;
	private GameObject _wallObjects;
	private GameObject _player;
	private Transform _cameraTransform;
	private List<GameObject> walls;

	private float _pixelToUnit = .32f;
	private float _timer;
	private int _leftRotation = 0;
	private int _rightRotation = 0;

	void Awake ()
	{
		_wall = Resources.Load("Prefabs/Wall") as GameObject;
		_wallObjects = GameObject.FindWithTag("WallObjects");
		_player = GameObject.FindWithTag("Entity/Player");
		_cameraTransform = _player.transform.GetChild(0).transform;
		walls = new List<GameObject>();
		GenerateEmptyGameObjects(7);
	}

	// Update is called once per frame

	void Update ()
	{
		if (generateOnTimer) { GenerateWallOnTimer(rate); }
		DestroyMap();
		RegisterSwipe();
		float rotation = _cameraTransform.rotation.eulerAngles.z;
		direction = rotation / 360.0f;



	}

	private void GenerateWallOnTimer(float interval)
	{
		if (_timer > interval)
		{
			GenerateEmptyGameObjects(1);
			_timer = 0;
		} else {
			_timer += Time.deltaTime;
		}
	}


	private void Calculate()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
		if (_abs > _threshold)
		{
			float _difference = -(_pointerDown.x - _pointerUp.x);
			float zRotation = (_difference < 0) ? 90 : -90;
			if (_difference < 0)
			{
				swipe = Swipe.LEFT;
			} else if (_difference > 0)
			{
				swipe = Swipe.RIGHT;
			} else {
				swipe = Swipe.NONE;
			}

			//swipe = (_difference < 0) ? Swipe.LEFT  : (_difference > 0 ) ? Swipe.RIGHT : Swipe.NONE;
			_cameraTransform.rotation *= Quaternion.Euler(new Vector3(0, 0, zRotation));
		}

		_pointerDown = Vector2.zero;
		_pointerUp = Vector2.zero;
	}


	/// <summary>
	///
	/// </summary>
	/// <param name="value"></param>
	public void GenerateEmptyGameObjects(int value)
	{
		for (int i = 0; i < value; i++)
		{
			_wallObjects.transform.position = _player.transform.position;
			GameObject _clone = Instantiate(_wall, -Vector2.up * 4.0f, Quaternion.identity) as GameObject;
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
		float _rotationDirection = (Random.Range(0, 2) == 0) ? -90 : 90;

		if (_modifier == Modifier.TRANSFORM) // USE THIS FOR MODIFING ENTIRE TRANSFORM
		{


			if (_previousObject != null)
			{
				_object.transform.localScale = new Vector2(_size, _height);
				_object.transform.rotation = _previousObject.transform.rotation;
				if (_rotationFreq < _rotationFreqNumber)
				{
					RotateWall(_object, _rotationDirection);
				}
				_object.transform.position = _previousObject.transform.GetChild(0).transform.position;
				PositionWall(_object, _previousObject);
			} else
			{
				_object.transform.localScale = new Vector2(_size, 300);
			}
		}
	}


	private void RotateWall(GameObject _currentWall, float targetRotation)
	{
		// -90 == right
		// 90 == left

		if (targetRotation == -90f)
		{
			if (_rightRotation < 2 )
			{
				_currentWall.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, targetRotation));
				_rightRotation++;
				_leftRotation = 0;
			} else
			{
				_currentWall.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -targetRotation));
				_leftRotation++;
				_rightRotation = 0;
			}
		} else if (targetRotation == 90f)
		{
			if (_leftRotation < 2)
			{
				_currentWall.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, targetRotation));
				_leftRotation++;
				_rightRotation = 0;
			} else {
				_currentWall.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -targetRotation));
				_rightRotation++;
				_leftRotation = 0;
			}
		}
	}






	public void PositionWall(GameObject _currentWall, GameObject _lastWallTransform)
	{

		Transform _previousWall = _lastWallTransform.transform;
		Vector3 _offsetedPosition = Vector3.zero;
		if (_previousWall != null)
		{
			float _wallRotation = _currentWall.transform.localRotation.eulerAngles.z;
			if (_wallRotation == 270f)
			{
				_offsetedPosition = new Vector3(-(_previousWall.transform.localScale.x / 2.0f * _pixelToUnit), 0, 0);
			} else if (_wallRotation == 90f)
			{
				_offsetedPosition = new Vector3((_previousWall.transform.localScale.x / 2.0f * _pixelToUnit), 0, 0);
			} else if (_wallRotation == 180f)
			{
				_offsetedPosition = new Vector3(0, (_previousWall.transform.localScale.x / 2.0f * _pixelToUnit), 0);
			} else {
				_offsetedPosition = new Vector3(0, -(_previousWall.transform.localScale.x / 2.0f * _pixelToUnit), 0);
			}



			_currentWall.transform.position += _offsetedPosition;
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

public enum Swipe
{
	RIGHT,
	LEFT,
	NONE,
}

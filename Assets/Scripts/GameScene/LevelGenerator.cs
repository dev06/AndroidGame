using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour {

	private GameController _gameController;

	private int _leftRotation = 0;
	private int _rightRotation = 0;
	private int _proximityGeneratedWall = 0;
	private int _activeWalls = 0;
	private float _pixelToUnit;
	private float _wallSpeed;
	private float _rotationFreq;
	private float _wallWidth;
	private float _minHeight;
	private float _maxHeight;

	private GameObject _pathResource;
	private GameObject _wallObjects;
	private GameObject _player;


	void Start()
	{
		Init();
	}

	/// <summary>
	/// Inits all the components and fields.
	/// </summary>
	private void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_pathResource = GameResources.Path_resource;
		_wallObjects = _gameController.wallObjects;
		_player = _gameController.player;
		_pixelToUnit = Constants.PixelToUnit;
		_wallSpeed = Constants.WallSpeed;
		_rotationFreq = Constants.RotationFrequency;
		_minHeight = Constants.MinWallHeight;
		_maxHeight = Constants.MaxWallHeight;
		_wallWidth = Constants.WallWidth;
	}

	/// <summary>
	/// Creates the path prefabs
	/// </summary>
	/// <param name="value"></param>
	public void GenerateEmptyGameObjects(int value)
	{
		if (_activeWalls < Constants.MaxWallsAtTime)
		{
			for (int i = 0; i < value; i++)
			{
				_wallObjects.transform.position = _player.transform.position;
				GameObject _clone = Instantiate(_pathResource, -Vector2.up * 4.0f, Quaternion.identity) as GameObject;
				_clone.name = "Path" + _clone.GetHashCode();
				_clone.tag = "Path";
				_clone.transform.parent = _wallObjects.transform;
				GameObject _lastClone = (_wallObjects.transform.childCount > 1) ? _wallObjects.transform.GetChild(_wallObjects.transform.childCount - 2).gameObject : null;
				_clone.GetComponent<Path>().previousWall = (_lastClone != null ) ? _lastClone : null;
				ModifyTransformForObjects(_clone, _lastClone);
			}
			_activeWalls += value;
		}

	}

	public void GenerateDeadEnd(int value)
	{
		if (_activeWalls < Constants.MaxWallsAtTime)
		{
			for (int i = 0; i < value; i++)
			{
				_wallObjects.transform.position = _player.transform.position;
				GameObject _clone = Instantiate(_pathResource, -Vector2.up * 4.0f, Quaternion.identity) as GameObject;
				_clone.name = "Path" + _clone.GetHashCode();
				_clone.tag = "Path";
				_clone.transform.parent = _wallObjects.transform;
				GameObject _lastClone = (_wallObjects.transform.childCount > 1) ? _wallObjects.transform.GetChild(_wallObjects.transform.childCount - 2).gameObject : null;
				_clone.GetComponent<Path>().previousWall = (_lastClone != null ) ? _lastClone : null;
				ModifyTransformForObjects(_clone, _lastClone, new Vector3(i * 3 , -i, 0));
			}
			_activeWalls += value;
		}

	}

	/// <summary>
	/// Modifies the scale and rotation for the "current" object in respect to the "previous"
	/// </summary>
	/// <param name="_object"></param>
	/// <param name="_previousObject"></param>
	public void ModifyTransformForObjects(GameObject _object, GameObject _previousObject)
	{
		// _object = current path being modified
		// _previous = path before _object or the last path in the collection
		float _height = Random.Range(_minHeight, _maxHeight);
		float _rotationFreqNumber = Random.Range(0.0f, 1.1f);
		float _rotationDirection = (Random.Range(0, 2) == 0) ? -90 : 90;

		if (_previousObject != null)
		{
			// setting localscale and rotation
			_object.transform.localScale = new Vector2(_wallWidth, _height);
			_object.transform.rotation = _previousObject.transform.rotation;
			//rotating if true
			if (_rotationFreq > _rotationFreqNumber)
			{
				RotateWall(_object, _rotationDirection);
			}
			//setting current path position to the hinge of the previous object
			_object.transform.position = _previousObject.transform.GetChild(0).transform.position;
			//positioning the wall
			PositionWall(_object, _previousObject);
		} else
		{
			_object.transform.localScale = new Vector2(_wallWidth, Constants.InitWallSize);
		}

		Path _path_c =	_object.GetComponent<Path>();
		_path_c.collectible_verticalOffset = 0;
		_path_c.collectible_direction =  (Random.Range(0, 2) == 0) ? -1 : 1;

		for (int i = 0; i < _object.transform.childCount; i++)
		{
			Collectible _collectible = _object.transform.GetChild(i).GetComponent<Collectible>();
			if (_object.transform.GetChild(i).gameObject.tag.Contains("Collectible"))
			{
				int _direction = _object.GetComponent<Path>().collectible_direction;
				_gameController.collectibleController.ModifyCollectibleTransform(_object.transform.GetChild(i).gameObject, _object, _direction);
			}
		}
	}


	public void ModifyTransformForObjects(GameObject _object, GameObject _previousObject, Vector3 _offset)
	{
		// _object = current path being modified
		// _previous = path before _object or the last path in the collection
		float _height = Random.Range(_minHeight, _maxHeight);
		float _rotationFreqNumber = Random.Range(0.0f, 1.1f);
		float _rotationDirection = (Random.Range(0, 2) == 0) ? -90 : 90;

		if (_previousObject != null)
		{
			// setting localscale and rotation
			_object.transform.localScale = new Vector2(_wallWidth, _height);
			_object.transform.rotation = _previousObject.transform.rotation;

			//setting current path position to the hinge of the previous object
			_object.transform.position = _previousObject.transform.GetChild(0).transform.position + _offset;
			//positioning the wall
			PositionWall(_object, _previousObject);
		} else
		{
			_object.transform.localScale = new Vector2(_wallWidth, Constants.InitWallSize);
		}

		Path _path_c =	_object.GetComponent<Path>();
		_path_c.collectible_verticalOffset = 0;
		_path_c.collectible_direction =  (Random.Range(0, 2) == 0) ? -1 : 1;

		for (int i = 0; i < _object.transform.childCount; i++)
		{
			Collectible _collectible = _object.transform.GetChild(i).GetComponent<Collectible>();
			if (_object.transform.GetChild(i).gameObject.tag.Contains("Collectible"))
			{
				int _direction = _object.GetComponent<Path>().collectible_direction;
				_gameController.collectibleController.ModifyCollectibleTransform(_object.transform.GetChild(i).gameObject, _object, _direction);
			}
		}
	}

	/// <summary>
	/// Lerps the path rotation to the specified rotation.
	/// </summary>
	/// <param name="_currentWall"></param>
	/// <param name="targetRotation"></param>
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
	/// <summary>
	/// Applies the offset the path
	/// </summary>
	/// <param name="_currentWall"></param>
	/// <param name="_lastWallTransform"></param>
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
			_currentWall.transform.position  += _offsetedPosition;
		}
	}

	public void DestroyWall(GameObject _object)
	{
		Destroy(_object, 1.5f);
		_activeWalls--;
	}
}

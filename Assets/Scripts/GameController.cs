using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization


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

		GenerateWallMap();
	}

	// Update is called once per frame
	float counter = 0;
	void Update ()
	{

		// counter += Time.deltaTime;
		// if (counter > 1.0f)
		// {
		// 	GenerateWallMap();
		// 	counter = 0;
		// }
		DestroyMap();

		if (Input.GetMouseButtonDown(0))
		{
			_pointerDown = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_pointerUp = Input.mousePosition;
			Calculate();
		}

		float rotation = _wallObjects.transform.rotation.eulerAngles.z;
		direction = rotation / 360.0f;



		if (Input.GetKeyDown(KeyCode.Q)) {
			_wallObjects.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -90));
			_player.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -90));

		} else if (Input.GetKeyDown(KeyCode.E)) {
			_wallObjects.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
			_player.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));

		}
	}

	public static float direction;
	private void Calculate()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
		if (_abs > _threshold)
		{
			float _difference = -(_pointerDown.x - _pointerUp.x);
			if (_difference < 0 )
			{

				_wallObjects.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, -90));

			} else if (_difference > 0)
			{


				_wallObjects.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));

			}


		}

		_pointerDown = Vector2.zero;
		_pointerUp = Vector2.zero;


	}



	private void GenerateWallMap()
	{

		_wallObjects.transform.position = _player.transform.position;
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
			PositionWall(_currentWall, _lastwall, _rotated);
		}


	}

	private void PositionWall(GameObject _currentWall, GameObject _lastWallTransform, bool _rotated)
	{
		if (_lastWallTransform != null)
		{
			Transform _previousWall = _lastWallTransform.transform;
			_currentWall.transform.position = _previousWall.GetChild(0).transform.position;
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

			GenerateWallMap();
		}
	}

}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization
	private Vector2 _pointerDown;
	private Vector2 _pointerUp;


	private int _threshold = 20;
	private GameObject _wall;
	private List<GameObject> walls;

	private float _pixelToUnit = .32f;

	void Start ()
	{
		_wall = Resources.Load("Prefabs/Wall") as GameObject;
		walls = new List<GameObject>();
		//GenerateMap();
		GenerateWallMap();
	}

	// Update is called once per frame
	float counter;
	int index = 1;
	void Update ()
	{
		Camera.main.transform.position += new Vector3(0, .005f, 0);
		counter += Time.deltaTime;
		if (counter > .05f)
		{


			Destroy();
			index++;
			counter = 0;
		}

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


	private void Calculate()
	{
		float _abs = Mathf.Abs(_pointerUp.x - _pointerDown.x);
		if (_abs > _threshold)
		{
			float _difference = -(_pointerDown.x - _pointerUp.x);
			if (_difference < 0)
			{
				Debug.Log("Swipe Left");
			} else if (_difference > 0)
			{
				Debug.Log("Swipe Right");
			}
		}

		_pointerDown = Vector2.zero;
		_pointerUp = Vector2.zero;
	}

	float height;
	private void GenerateMap()
	{
		for (int i = 0; i < index	; i++)
		{
			height = Random.Range(.01f, .02f);
			if (i > 0)
			{
				Transform _previousWall = walls[i - 1].transform;
				GameObject _currentWall = Instantiate(_wall, new Vector2( 0,  0), Quaternion.identity) as GameObject;
				_currentWall.name = "Wall" + i;
				_currentWall.tag = "Walls";
				Vector2 pos = Vector2.zero;
				if (Random.Range(0, 2) == 0)
				{
					float rotation =  (Random.Range(0, 2) == 0) ? -90 : 90;
					_currentWall.transform.Rotate(new Vector3(0, 0, rotation));
					pos = new Vector2((rotation > 0) ? (_previousWall.transform.localScale.x / 2.0f * .32f) : -(_previousWall.transform.localScale.x / 2.0f * .32f) , _previousWall.transform.position.y + (_previousWall.transform.localScale.y * .32f) + (_previousWall.transform.localScale.x / 2.0f) * .32f);
				} else
				{
					pos = new Vector2(0, _previousWall.transform.position.y + (_previousWall.transform.localScale.y * .32f));
				}

				_currentWall.transform.position = pos;
				_currentWall.transform.localScale = new Vector2(.2f, height);
				walls.Add(_currentWall);

			} else {

				GameObject _currentWall = Instantiate(_wall, new Vector2(0, 0), Quaternion.identity) as GameObject;
				_currentWall.transform.localScale = new Vector2(.2f, height );
				_currentWall.tag = "Walls";
				walls.Add(_currentWall);

			}


			//GameObject _currentWall = Instantiate(_wall, new Vector2(0, i * .32f), Quaternion.identity) as GameObject;
		}
	}

	public float _rotationFreq;
	public float _size;
	public float _minHeight;
	public float _maxHeight;

	private void GenerateWallMap()
	{
		for (int i = 0; i < index; i++)
		{
			GameObject _currentWall = Instantiate(_wall, new Vector2(0, -.5f), Quaternion.identity) as GameObject;
			_currentWall.transform.parent = GameObject.Find("WallObjects").transform;
			float _height = Random.Range(_minHeight, _maxHeight);
			bool _rotated = false;
			if ( i > 0)
			{
				if (Random.Range(0.0f, 1.1f) < _rotationFreq)
				{
					if (walls[i - 1].transform.rotation.eulerAngles.z != 0)
					{
						Quaternion _rotation = walls[i - 1].transform.rotation;
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
			PositionWall(_currentWall, i, _rotated);
		}
	}

	private void PositionWall(GameObject _currentWall, int _currentIndex, bool _rotated)
	{
		if (_currentIndex > 0)
		{
			Transform _previousWall = walls[_currentIndex - 1].transform;

			_currentWall.transform.position = _previousWall.GetChild(0).transform.position;

			if (_currentWall.transform.rotation.eulerAngles.z != 0)
			{
				_currentWall.transform.position = new Vector3(_currentWall.transform.position.x, _currentWall.transform.position.y + _previousWall.transform.localScale.x / 2.0f * _pixelToUnit);
				if (_currentWall.transform.rotation.eulerAngles.z > 180) {
					_currentWall.transform.position = new Vector3(_currentWall.transform.position.x - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit, _currentWall.transform.position.y - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit);
				} else {
					_currentWall.transform.position = new Vector3(_currentWall.transform.position.x + _currentWall.transform.localScale.x / 2.0f * _pixelToUnit, _currentWall.transform.position.y - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit);

				}

			} else {
				_currentWall.transform.position = new Vector3(_currentWall.transform.position.x , _currentWall.transform.position.y - _currentWall.transform.localScale.x / 2.0f * _pixelToUnit);
			}

		}

	}


	private void Destroy()
	{

		// if (Input.GetMouseButtonDown(1))
		// {
		// 	if (walls.Count > 0)
		// 	{
		// 		GameObject[] _walls = GameObject.FindGameObjectsWithTag("Walls");
		// 		for (int i = 0; i < _walls.Length; i++)
		// 		{
		// 			Destroy(_walls[i]);
		// 		}

		// 		walls.Clear();


		// 	}

		// 	GenerateWallMap();


		// }


		if (Input.GetMouseButtonDown(0))
		{
			GameObject.Find("WallObjects").transform.rotation *= Quaternion.Euler(new Vector3(0, 0, 90));
		}

		if (walls.Count > 0)
		{
			GameObject[] _walls = GameObject.FindGameObjectsWithTag("Walls");
			for (int i = 0; i < _walls.Length; i++)
			{
				Destroy(_walls[i]);
			}

			walls.Clear();

			GenerateWallMap();

		}


	}

}

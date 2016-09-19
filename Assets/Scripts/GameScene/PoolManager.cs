using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour {

	private GameController _gameController;
	private LevelGenerator _levelGenerator;
	private GameObject _wallObjects;
	private static float _delayTimer;
	private static bool _startTimer;
	void Start ()
	{
		Init();
	}

	void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_levelGenerator = _gameController.levelGenerator;
		_wallObjects = GameObject.FindWithTag("WallObjects");
	}

	public void ShiftElements(GameObject _objects)
	{
		for (int i = 0; i < _objects.transform.childCount; i++)
		{
			_objects.transform.GetChild(i).SetSiblingIndex(i + 1);
		}
	}

	GameObject current;
	GameObject previous;
	void Update()
	{
		_delayTimer = (_startTimer) ? _delayTimer + Time.deltaTime : 0;

		if (_delayTimer > .22f)
		{
			if (previous != null)
			{
				ShiftElements(_wallObjects);
				_levelGenerator.ModifyTransformForObjects(current, previous);
				_startTimer = false;
				_delayTimer = 0;
			}
		}
	}


	public void PoolObject(GameObject current, GameObject previous)
	{

		_startTimer = true;
		this.current = current;
		this.previous = previous;

	}


}

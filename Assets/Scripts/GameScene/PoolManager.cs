using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour {


	private GameController _gameController;
	private LevelGenerator _levelGenerator;
	private GameObject _wallObjects;
	public GameObject pathReserve;


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



	public void PoolPath(GameObject current, GameObject previous)
	{
		if (EventManager.OnPooled != null)
		{
			EventManager.OnPooled();
		}

		ShiftElements(_wallObjects);
		_levelGenerator.ModifyTransformForObjects(current, previous);
		if (previous != null)
		{

		}
	}


}

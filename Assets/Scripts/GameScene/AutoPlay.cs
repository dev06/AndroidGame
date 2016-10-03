using UnityEngine;
using System.Collections;

public class AutoPlay : MonoBehaviour {

	public AutoPlayID autoPlayID;
	private GameController _gameController;
	private Player _player;
	private static int _directionIndex;
	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_player = transform.parent.GetComponent<Player>();
	}

	void Update ()
	{
		// if (autoPlayID == AutoPlayID.Colliders)
		// {
		// 	if (_gameController.autoPlay)
		// 	{
		// 		if (_directionIndex < 0)
		// 		{
		// 			_directionIndex = Constants.directions.Length - 1;
		// 		} else if (_directionIndex > Constants.directions.Length - 1)
		// 		{
		// 			_directionIndex = 0;
		// 		}

		// 		_gameController.facingDirection = Constants.directions[_directionIndex];
		// 	}
		// }
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// if (autoPlayID == AutoPlayID.Colliders)
		// {
		// 	if (_gameController.autoPlay)
		// 	{
		// 		if (col.gameObject.tag == "Path")
		// 		{
		// 			if (gameObject.name == "Right" )
		// 			{
		// 				_directionIndex =  _directionIndex +  1 ;
		// 			} else if (gameObject.name == "Left")
		// 			{
		// 				_directionIndex =  _directionIndex -  1 ;

		// 			}
		// 		}
		// 	}
		// }
	}

	void OnDisable()
	{
		_directionIndex = 0;
	}


}

public enum AutoPlayID
{
	Indicators,
	Colliders,
}

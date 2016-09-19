using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization

	public LevelGenerator levelGenerator;
	public GameInput gameInput;
	public GameObject wallObjects;
	public GameObject player;
	public Direction facingDirection;
	public static float direction;

	public bool autoPlay;





	void Awake ()
	{
		Init();
	}

	private void Init()
	{
		wallObjects = GameObject.FindWithTag("WallObjects");
		player = GameObject.FindWithTag("Entity/Player");
		levelGenerator = GetComponent<LevelGenerator>();
		gameInput = GetComponent<GameInput>();
		gameInput.gameController = this;
	}


	void Update ()
	{

		UpdateFacingDirection();
		gameInput.RegisterSwipe();

	}






	private void UpdateFacingDirection()
	{
		float rotation = Camera.main.transform.rotation.eulerAngles.z;
		direction = rotation / 360.0f;
		facingDirection = (direction == 0) ? Direction.NORTH : (direction == .25f) ? Direction.WEST : (direction == .5f) ? Direction.SOUTH : Direction.EAST;
	}




}

public enum Direction
{
	NORTH,
	SOUTH,
	EAST,
	WEST
}



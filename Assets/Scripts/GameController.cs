using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	// Use this for initialization

	public LevelGenerator levelGenerator;
	public PoolManager poolManager;
	public GameInput gameInput;
	public GameObject wallObjects;
	public GameObject player;
	public Direction facingDirection;
	public static float direction;
	public bool autoPlay;


	public Transform cameraTransform;


	public static float timer;
	public static bool timerBool;


	void Awake ()
	{
		Init();
	}

	private void Init()
	{
		wallObjects = GameObject.FindWithTag("WallObjects");
		player = GameObject.FindWithTag("Entity/Player");
		levelGenerator = GetComponent<LevelGenerator>();
		poolManager = GetComponent<PoolManager>();
		gameInput = GetComponent<GameInput>();
<<<<<<< HEAD
		gameInput.gameController = this;
=======
		cameraTransform = Camera.main.transform;
>>>>>>> 5e79c8ba428ab43c4bd018bcfb45b61f17893a39
	}


	void Update ()
	{
		if (timerBool)
		{
			timer += Time.deltaTime;
		}

		if (timer > .4f)
		{
			timerBool = false;
			timer = 0;
		}


		UpdateFacingDirection();
		gameInput.RegisterSwipe();

	}






	private void UpdateFacingDirection()
	{
		float rotation = cameraTransform.rotation.eulerAngles.z;
		direction = rotation / 360.0f;
		//facingDirection = (direction == 0) ? Direction.NORTH : (direction == .25f) ? Direction.WEST : (direction == .5f) ? Direction.SOUTH : Direction.EAST;
	}




}

public enum Direction
{
	NORTH,
	SOUTH,
	EAST,
	WEST
}



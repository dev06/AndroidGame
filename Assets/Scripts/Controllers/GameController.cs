using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {


	public static float direction;
	public static float timer;
	public static bool timerBool;


	public LevelGenerator levelGenerator;
	public PoolManager poolManager;
	public GameInput gameInput;
	public Direction facingDirection;

	public GameObject wallObjects;
	public GameObject player;
	public Transform cameraTransform;
	public bool autoPlay;
	public bool move;



	#region--- DEBUG MEMBERS----
	public bool endGameUponDeath;
	#endregion--- /DEBUG MEMBERS----
	void Awake ()
	{
		Init();
	}

	private void Init()
	{
		InitPlayer();
		wallObjects = GameObject.FindWithTag("WallObjects");
		player = GameObject.FindWithTag("Entity/Player");
		levelGenerator = GetComponent<LevelGenerator>();
		poolManager = GetComponent<PoolManager>();
		gameInput = GetComponent<GameInput>();
		gameInput.gameController = this;
		cameraTransform = Camera.main.transform;
	}


	void Update ()
	{
		UpdateFacingDirection();
		gameInput.RegisterSwipe();
	}

	private void InitPlayer()
	{
		Destroy(GameObject.FindWithTag("Camera/DummyCamera"));
		Instantiate(GameResources .Player_resource, Vector3.zero, Quaternion.identity);

	}


	private void UpdateFacingDirection()
	{
		float rotation = cameraTransform.rotation.eulerAngles.z;
		direction = rotation / 360.0f;
	}
}

public enum Direction
{
	NORTH,
	SOUTH,
	EAST,
	WEST
}



using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	public static GameController Instance;
	public static float direction;
	public static float timer;
	public static bool timerBool;


	public LevelGenerator levelGenerator;
	public PoolManager poolManager;
	public CollectibleController collectibleController;
	public GameInput gameInput;
	public Direction facingDirection;

	public GameObject wallObjects;
	public GameObject player;
	public Transform cameraTransform;
	public bool autoPlay;
	public bool move;
	public bool dead;
	public GameState gameState;

	public CanvasManager canvasManager;

	#region--- DEBUG MEMBERS----
	public bool endGameUponDeath;
	#endregion--- /DEBUG MEMBERS----
	void Awake ()
	{
		Init();
	}

	private void Init()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}

		InitPlayer();
		wallObjects = GameObject.FindWithTag("WallObjects");
		player = GameObject.FindWithTag("Entity/Player");
		levelGenerator = GetComponent<LevelGenerator>();
		poolManager = GetComponent<PoolManager>();
		gameInput = GetComponent<GameInput>();
		canvasManager = GameObject.FindWithTag("Manager/CanvasManager").GetComponent<CanvasManager>();
		collectibleController = GameObject.FindWithTag("Manager/CollectibleController").GetComponent<CollectibleController>();

		gameInput.gameController = this;
		gameState = GameState.DEBUG;

		cameraTransform = Camera.main.transform;
		EventManager.OnDoubleTap += OnDoubleTap;
	}

	void Update ()
	{
		if (!dead)
		{
			UpdateFacingDirection();
			gameInput.RegisterSwipe();

		}
	}

	public void InitPlayer()
	{
		Destroy(GameObject.FindWithTag("Camera/DummyCamera"));
		Instantiate(GameResources .Player_resource, Vector3.zero, Quaternion.identity);
	}


	private void UpdateFacingDirection()
	{
		float rotation = cameraTransform.rotation.eulerAngles.z;
		direction = rotation / 360.0f;
	}

	public void OnDoubleTap()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
	}
	void OnDisable()
	{
		PlayerPrefs.SetFloat("WallSpeed", Constants.WallSpeed);
		PlayerPrefs.SetFloat("RotationFrequency", Constants.RotationFrequency);
		PlayerPrefs.SetFloat("MinWallHeight", Constants.MinWallHeight);
		PlayerPrefs.SetFloat("MaxWallHeight", Constants.MaxWallHeight);
	}

	public void SpawnItem()
	{
		GameObject _item = Instantiate(GameResources.Square_resource, Vector3.zero, Quaternion.identity) as GameObject;
		Transform _lastPath = wallObjects.transform.GetChild(wallObjects.transform.childCount - 1).transform.GetChild(0).transform;
		_item.transform.parent = _lastPath.transform.parent.transform;
		float _pathWidth = _lastPath.parent.transform.localScale.x;
		float _pathHeight = _lastPath.parent.transform.localScale.y;
		Vector3 scale = (_lastPath.eulerAngles.z == 0 || _lastPath.eulerAngles.z == 180) ? new Vector3(1.0f, _pathWidth / _pathHeight, 1.0f) : new Vector3(_pathWidth / _pathHeight, 1.0f, 1.0f);
		_item.transform.localScale = scale;
		Vector3 _itemPos =  new Vector3(_pathWidth + _item.transform.localScale.x / 2.0f * Constants.PixelToUnit, Random.Range(0f, .32f), 0);
		_item.transform.localPosition = _itemPos;
	}
}

public enum Direction
{
	NORTH,
	SOUTH,
	EAST,
	WEST
}

public enum GameState
{
	DEBUG,
	GAME,
}



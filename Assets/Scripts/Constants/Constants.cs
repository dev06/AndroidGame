using UnityEngine;
using System.Collections;

public class Constants
{

	public static float PixelToUnit = .32f;

	// lEVEL GENERATION FIELDS

	public static float WallSpeed = 15f;//26.5F;
	public static float WallWidth = .75f;
	public static float RotationFrequency = .5f;
	public static float MinWallHeight = 50;
	public static float MaxWallHeight = 75;
	public static float InitWallSize = 100;
	public static float SwipePause = .2f;

	public static int MaxWallsAtTime = 7;



	// GAME INPUT
	public static float SwipeThresHold = 20;
	public static float SwipeDelay = .5f;


	//DIRECTION

	public static Direction[] directions = new Direction[4] {Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST};

}


public class GameResources
{
	public static GameObject Path_resource = Resources.Load("Prefabs/prefabs_path/path") as GameObject;
	public static GameObject Player_resource = Resources.Load("Prefabs/prefabs_character/character") as GameObject;

}

using UnityEngine;
using System.Collections;

public class Constants
{

	public static float PixelToUnit = .32f;

	// lEVEL GENERATION FIELDS




	public static float WallSpeed = 15f;//26.5F;
	public static float WallWidth = .75f;
	public static float RotationFrequency = 1f;
	public static float MinWallHeight = 50;
	public static float MaxWallHeight = 75;
	public static float InitWallSize = 100;
	public static int MaxWallsAtTime = 7;
	public static int ProximityGenereatedWalls = 3;




	// GAME INPUT
	public static float SwipeThresHold = 20;


	//DIRECTION

	public static Direction[] directions = new Direction[4] {Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST};

}

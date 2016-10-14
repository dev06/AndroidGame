using UnityEngine;
using System.Collections;

public class Constants
{

	public static float PixelToUnit = .32f;

	// lEVEL GENERATION FIELDS
	public static int InitWallGenerationSize = 7;
	public static float WallSpeed = 30f;//26.5F;
	public static float WallWidth = .75f;
	public static float RotationFrequency = 1f;
	public static float MinWallHeight = 35;
	public static float MaxWallHeight = 65;
	public static float InitWallSize = 40;
	public static float SwipePause = .2f;

	public static int MaxWallsAtTime = 7;



	// GAME INPUT
	public static float SwipeThresHold = 20;
	public static float SwipeDelay = .5f;
	public static float DoubleTapDelay = .3f;


	//Debug

	//DIRECTION

	public static Direction[] directions = new Direction[4] {Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST};

	//Particle
	public static float GridParticleVelocity = -2.5f;

}


public class GameResources
{
	public static GameObject Path_resource = Resources.Load("Prefabs/prefabs_path/path") as GameObject;
	public static GameObject Player_resource = Resources.Load("Prefabs/prefabs_character/character") as GameObject;
	public static GameObject GridParticle_resource = Resources.Load("Prefabs/prefabs_particles/prefabs_grid_particle/GridParticle") as GameObject;
	public static GameObject Square_resource = Resources.Load("Prefabs/prefabs_item/Square") as GameObject;
	public static GameObject Diamond_resource = Resources.Load("Prefabs/prefabs_item/item_diamond/Diamond") as GameObject;


}

using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void  Swipe();
	public static Swipe OnSwipe;


	public delegate void Pooled();
	public static Pooled OnPooled;


	public delegate void Gesture();
	public static Gesture OnSwipeRight;
	public static Gesture OnSwipeLeft;
	public static Gesture OnTap;
	public static Gesture OnDoubleTap;


	public delegate void Death();
	public static Death OnDeath;


	public delegate void CollectibleTransformModified();
	public static CollectibleTransformModified OnModifyTransform;




}

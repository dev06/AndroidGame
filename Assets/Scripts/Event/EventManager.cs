using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void  Swipe();
	public static Swipe OnSwipe;

}

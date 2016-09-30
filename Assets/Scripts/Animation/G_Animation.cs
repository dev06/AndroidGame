using UnityEngine;
using System.Collections;

public class G_Animation : MonoBehaviour {


	public static void Play(Animation _anim, string name, float speed, float direction)
	{
		_anim[name].speed = speed;
		_anim[name].time = (direction > 0) ? 0 : _anim[name].length;
		_anim.Play(name);
	}
}

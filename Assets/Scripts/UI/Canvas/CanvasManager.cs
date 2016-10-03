using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{

	public List<GameObject> canvas = new List<GameObject>();

	public void ActivateCanvas(CanvasID _canvasID, bool value)
	{
		for (int i = 0; i < canvas.Count; i++)
		{
			if (canvas[i].GetComponent<CanvasHandler>().canvasID == _canvasID)
			{
				canvas[i].GetComponent<Canvas>().enabled = value;
			}
		}
	}
}

public enum CanvasID
{
	DEBUG,
}

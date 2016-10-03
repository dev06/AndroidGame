using UnityEngine;
using System.Collections;

public class CanvasHandler : MonoBehaviour
{

	public CanvasID canvasID;
	private GameController _gameController;

	void Start()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_gameController.canvasManager.canvas.Add(this.gameObject);
		_gameController.canvasManager.ActivateCanvas(CanvasID.DEBUG, true);

	}
}

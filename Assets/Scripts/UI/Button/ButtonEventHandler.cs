using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonEventHandler : MonoBehaviour, IPointerClickHandler {

	private GameController _gameController;

	void Start ()
	{
		Init();
	}

	protected void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}
	void Update () {

	}

	public virtual void OnPointerClick(PointerEventData data)
	{
		_gameController.canvasManager.ActivateCanvas(CanvasID.DEBUG, false);

		_gameController.levelGenerator.GenerateEmptyGameObjects(3);

		StartCoroutine("StateChange");
	}

	IEnumerator StateChange()
	{
		yield return new WaitForSeconds(1f);
		_gameController.gameState = GameState.GAME;
	}
}

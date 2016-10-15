using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Collectible : MonoBehaviour {

	public GameItem _gameItem;

	protected GameController _gameController;
	protected SpriteRenderer _collectibleSprite;

	void OnEnable()
	{
		EventManager.OnModifyTransform += OnModifyTransform;
	}

	void Start ()
	{
		Init();
	}

	protected void Init()
	{
		_gameController = GameController.Instance;
		_collectibleSprite = transform.GetComponent<SpriteRenderer>();
		Debug.Log("Initilized");
	}

	void Update ()
	{

	}

	public virtual void SetActive(bool _value)
	{
		if (_collectibleSprite != null)
		{
			_collectibleSprite.enabled = _value;
		}
	}

	public virtual void OnModifyTransform()
	{

	}

	void OnDisable()
	{
		EventManager.OnModifyTransform -= OnModifyTransform;
	}

}

public enum GameItem
{
	Diamond,
}

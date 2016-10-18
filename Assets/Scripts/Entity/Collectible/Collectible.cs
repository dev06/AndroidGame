using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Collectible : MonoBehaviour {

	public GameItem _gameItem;
	protected GameObject _parentObject; // The object that this collectibe is a child of.
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

	public virtual void SetParent(GameObject _parent)
	{
		_parentObject = _parent;
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

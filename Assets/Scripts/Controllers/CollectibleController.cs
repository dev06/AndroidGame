using UnityEngine;
using System.Collections;

public class CollectibleController : MonoBehaviour {

	private GameController _gameController;
	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}


	public void SpawnCollectible(int amount)
	{
		float yOffset = 0 ;
		for (int i = 0; i < amount; i++)
		{
			GameObject _item = Instantiate(GameResources.Square_resource, Vector3.zero, Quaternion.identity) as GameObject;
			Transform _lastHinge = _gameController.wallObjects.transform.GetChild(_gameController.wallObjects.transform.childCount - 1).transform.GetChild(0).transform;
			_item.transform.parent = _lastHinge.transform.parent.transform;
			float _pathWidth = _lastHinge.parent.transform.localScale.x;
			float _pathHeight = _lastHinge.parent.transform.localScale.y;
			Vector3 scale = (_lastHinge.eulerAngles.z == 0 || _lastHinge.eulerAngles.z == 180) ? new Vector3(1.0f, _pathWidth / _pathHeight, 1.0f) : new Vector3(_pathWidth / _pathHeight, 1.0f, 1.0f);
			_item.transform.localScale = scale;
			yOffset += (Constants.PixelToUnit / 2.0f) / amount;
			Vector3 _itemPos = Vector3.zero;
			if (i % 2 == 0)
			{
				_itemPos =  new Vector3(_pathWidth + _item.transform.localScale.x / 2.0f * Constants.PixelToUnit, yOffset , 0);
			} else {
				_itemPos =  new Vector3(-_pathWidth - _item.transform.localScale.x / 2.0f * Constants.PixelToUnit, yOffset , 0);
			}
			_item.transform.localPosition = _itemPos;
		}
	}

	public void GenerateCollectible(GameObject _parentObject, int amount)
	{
		float _verticalOffset = 0;
		_parentObject.transform.GetComponent<Path>().collectible_verticalOffset = 0f;
		for (int i = 0; i < amount; i++)
		{
			GameObject _collectible = Instantiate(GameResources.Diamond_resource, Vector3.zero, Quaternion.identity) as GameObject;
			_collectible.transform.parent = _parentObject.transform;

			ModifyCollectibleTransform(_collectible, _parentObject);
		}
	}

	/// <summary>
	/// Modifies the transform including postion, scale and possible rotation for the collectibe
	/// </summary>
	/// <param name="_collectible"></param>
	public void ModifyCollectibleTransform(GameObject _collectible, GameObject _parentObject)
	{
		Transform _parentTransform = _parentObject.transform;
		float _pathWidth = _parentTransform.localScale.x;
		float _pathHeight = _parentTransform.localScale.y;
		_parentTransform.GetComponent<Path>().collectible_verticalOffset += (Constants.PixelToUnit / _parentTransform.GetComponent<Path>().collectible_amount);
		_parentTransform.GetComponent<Path>().collectible_direction *= -1;

		//Vector3 scale = (_parentTransform.eulerAngles.z == 0 || _parentTransform.eulerAngles.z == 180) ? new Vector3(1.0f, _pathWidth / _pathHeight, 1.0f) : new Vector3(_pathWidth / _pathHeight, 1.0f, 1.0f);
		Vector3 scale = new Vector3(1.0f, _pathWidth / _pathHeight, 1.0f);
		_collectible.transform.localScale = scale;
		Vector3 _itemPos = Vector3.zero;
		int direction = _parentTransform.GetComponent<Path>().collectible_direction;
		_itemPos =  new Vector3((direction * _pathWidth) + (direction * (_collectible.transform.localScale.x / 10.0f * Constants.PixelToUnit)), _parentTransform.GetComponent<Path>().collectible_verticalOffset - ((Constants.PixelToUnit / 2.0f) / _parentTransform.GetComponent<Path>().collectible_amount) , 0);
		_collectible.transform.localPosition = _itemPos;
		_collectible.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}
}

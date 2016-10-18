using UnityEngine;
using System.Collections;

public class CollectibleController : MonoBehaviour {

	private GameController _gameController;
	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	/// <summary>
	/// Generates x collectibles under the parent parent object
	/// </summary>
	/// <param name="_parentObject"></param>
	/// <param name="amount"></param>
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

		Vector3 scale = new Vector3(1.0f, _pathWidth / _pathHeight, 1.0f);
		_collectible.transform.localScale = scale;
		Vector3 _itemPos = Vector3.zero;

		// direction is use to flip the collectibles on different side of the path.
		int direction = _parentTransform.GetComponent<Path>().collectible_direction;
		float _itemPosX = (direction * _pathWidth) + (direction * (_collectible.transform.localScale.x / 100.0f * Constants.PixelToUnit));
		float _itemPosY = _parentTransform.GetComponent<Path>().collectible_verticalOffset - ((Constants.PixelToUnit / 2.0f) / _parentTransform.GetComponent<Path>().collectible_amount);

		_itemPos =  new Vector3(_itemPosX, _itemPosY, 0);
		_collectible.transform.localPosition = _itemPos;
		_collectible.transform.localRotation = Quaternion.Euler(Vector3.zero);

		_collectible.GetComponent<Collectible>().SetParent(_parentObject);

	}
}

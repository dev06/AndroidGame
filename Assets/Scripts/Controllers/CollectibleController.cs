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
			GameObject _item = Instantiate(GameResources.Item_resource, Vector3.zero, Quaternion.identity) as GameObject;
			Transform _lastPath = _gameController.wallObjects.transform.GetChild(_gameController.wallObjects.transform.childCount - 1).transform.GetChild(0).transform;
			_item.transform.parent = _lastPath.transform.parent.transform;
			float _pathWidth = _lastPath.parent.transform.localScale.x;
			float _pathHeight = _lastPath.parent.transform.localScale.y;
			Vector3 scale = (_lastPath.eulerAngles.z == 0 || _lastPath.eulerAngles.z == 180) ? new Vector3(1.0f, _pathWidth / _pathHeight, 1.0f) : new Vector3(_pathWidth / _pathHeight, 1.0f, 1.0f);
			_item.transform.localScale = scale;
			yOffset += Constants.PixelToUnit / amount;
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
}

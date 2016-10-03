using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Debug_Member : MonoBehaviour
{

	public Debug_Member_ID id;
	private Slider _slider;
	private Text _valueText;


	void Start ()
	{
		_slider = transform.GetChild(0).GetComponent<Slider>();
		_valueText = transform.GetChild(2).GetComponent<Text>();

		InitValue();


	}

	void Update ()
	{
		_valueText.text = "" + _slider.value;
		SetValue();
	}

	public void SetValue()
	{
		switch (id)
		{
			case Debug_Member_ID.WallSpeed:
			{
				Constants.WallSpeed = _slider.value;
				break;
			}
			case Debug_Member_ID.RotationFrequency:
			{
				Constants.RotationFrequency = _slider.value;
				break;
			}

			case Debug_Member_ID.MinHeight:
			{
				Constants.MinWallHeight = _slider.value;
				break;
			}

			case Debug_Member_ID.MaxHeight:
			{
				Constants.MaxWallHeight = _slider.value ;
				break;
			}

		}

	}

	public void InitValue()
	{
		switch (id)
		{
			case Debug_Member_ID.WallSpeed:
			{
				_slider.value = PlayerPrefs.GetFloat("WallSpeed");
				break;
			}
			case Debug_Member_ID.RotationFrequency:
			{
				_slider.value = PlayerPrefs.GetFloat("RotationFrequency");
				break;
			}

			case Debug_Member_ID.MinHeight:
			{
				_slider.value =  PlayerPrefs.GetFloat("MinWallHeight");
				break;
			}

			case Debug_Member_ID.MaxHeight:
			{
				_slider.value =  PlayerPrefs.GetFloat("MaxWallHeight");
				break;
			}
		}
	}
}

public enum Debug_Member_ID
{
	Scale,
	WallSpeed,
	RotationFrequency,
	MinHeight,
	MaxHeight,
}

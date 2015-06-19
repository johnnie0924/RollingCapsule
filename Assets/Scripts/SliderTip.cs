using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderTip : MonoBehaviour {

	public Text countText;

	public void refreshValue()
	{
		countText.text = GetComponent<Slider> ().value.ToString("0") + "/" + GetComponent<Slider> ().maxValue.ToString("0");
	}
}

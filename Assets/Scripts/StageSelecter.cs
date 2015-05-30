using UnityEngine;
using System.Collections;

public class StageSelecter : MonoBehaviour {

	public string picName;
	public Texture2D picData;

	public void StartLoading () 
	{
		GameManager.instance.InitGame (picData);
	}
}

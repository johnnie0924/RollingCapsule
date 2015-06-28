using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageSelecter : MonoBehaviour {

	public Texture2D picData;
	public GameObject dispImage;

	void Awake () 
	{
		RefreshImage ();
	}

	public void StartLoading () 
	{
		GameManager.instance.InitGame (picData);
	}

	public void RefreshImage () 
	{
		dispImage.GetComponent<Image>().sprite = Sprite.Create(picData,new Rect(0, 0, picData.width, picData.height),Vector2.zero);
	}

	public void MakeDsabele () 
	{
		GetComponent<Button>().interactable = false;
	}

	public void MakeEnabele () 
	{
		GetComponent<Button>().interactable = true;
	}

}

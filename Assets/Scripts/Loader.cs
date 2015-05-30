using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loader : MonoBehaviour {
	
	public GameObject gameManager;
	public Texture2D[] picList;

	public RectTransform prefab = null;
	
	void Awake () 
	{
		if (GameManager.instance == null) 
		{
			Instantiate (gameManager);
		}
	}

	void Start()
	{
		for(int i=0; i < picList.Length; i++)
		{
			RectTransform item = GameObject.Instantiate(prefab) as RectTransform;
			item.GetComponent<StageSelecter>().picData = picList[i];

			item.SetParent(GameObject.FindGameObjectWithTag("StageSelectContent").transform, false);
			
			Text text = item.GetComponentInChildren<Text>();
			text.text = "item:" + i.ToString() + " name:" + picList[i].name;
		}
	}
}
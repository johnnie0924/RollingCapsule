using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	[HideInInspector] public GroundManager groundScript;
	[HideInInspector] public CameraController CameraController;
	[HideInInspector] public GameObject StageSelectCanvas;
	[HideInInspector] public GameObject AndroidInputCanvas;
	[HideInInspector] public Slider ProgressSlider;
	[HideInInspector] public GameObject FinalDisplay;
	[HideInInspector] public Image FinalImage;

	public GameObject player;

	[HideInInspector] public int targetCount = 1;
	[HideInInspector] public int nowCount = 1;

	public bool pressZ;
	public bool pressX;
	public bool pressC;
	public bool pressV;
	public bool pressSpace;

	void Awake () {

		if (instance == null) 
		{
			instance = this;
		}
		else if (instance != this) 
		{
			Destroy(instance);
		}

		DontDestroyOnLoad (instance);
		groundScript = GetComponent<GroundManager> ();
		CameraController = GetComponent<CameraController> ();
		StageSelectCanvas = GameObject.FindGameObjectWithTag ("StageSelectCanvas");
		AndroidInputCanvas = GameObject.FindGameObjectWithTag ("AndroidInputCanvas");
		ProgressSlider = GameObject.FindGameObjectWithTag ("ProgressSlider").GetComponent<Slider>();
		FinalDisplay = GameObject.FindGameObjectWithTag ("FinalDisplay");
		FinalImage = GameObject.FindGameObjectWithTag ("FinalImage").GetComponent<Image>();
		StageSelectCanvas.SetActive (true);
		AndroidInputCanvas.SetActive (false);
		FinalDisplay.SetActive (false);
	}

	void Update()
	{
		if (pressZ)
		{
			pressZ = false;
			CameraController.ChangeCamera ();
		}
	}

	public void UpdateProgressBar()
	{
		ProgressSlider.value = (float)nowCount / (float)targetCount;
	}

	public void InitGame(Texture2D picData)
	{
		groundScript.SetupScene (picData);
		FinalImage.sprite = Sprite.Create(picData,new Rect(0, 0, picData.width, picData.height),Vector2.zero);
		GameObject newPlayer = Instantiate (player, new Vector3(5f,10f,5f), Quaternion.identity) as GameObject;
		newPlayer.GetComponent<PlayerController> ().isMain = true;
		CameraController.InitializePosition(newPlayer);
		StageSelectCanvas.SetActive (false);
		AndroidInputCanvas.SetActive (true);
	}
	
	public void CheckIfGameClear()
	{
		if (nowCount >= targetCount) 
		{
			Debug.Log ("GameClear");
			CameraController.ChangeCamera();
			StartCoroutine(SubCamMove(5.0f));
			FinalDisplay.SetActive (true);
		}
	}

	IEnumerator SubCamMove(float overTime)
	{
		float t = 0;
		Vector3 source = new Vector3(GameManager.instance.groundScript.columns / 2f, 0f,GameManager.instance.groundScript.rows / 2f);
		Vector3 target = new Vector3(GameManager.instance.groundScript.columns / 2f,(GameManager.instance.groundScript.rows + GameManager.instance.groundScript.columns) / 2f + 40f,GameManager.instance.groundScript.rows / 2f);

		while(t < 1)
		{
			yield return null;
			t = t + Time.deltaTime / overTime;
			CameraController.subCam.transform.position = Vector3.Lerp(source, target, t);
		}
		transform.position = target;
	}

}

using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	[HideInInspector] public GroundManager groundScript;
	[HideInInspector] public CameraController CameraController;
	[HideInInspector] public GameObject StageSelectCanvas;
	[HideInInspector] public GameObject AndroidInputCanvas;
	[HideInInspector] public GameObject ProgressSlider;
	[HideInInspector] public GameObject Timer;
	[HideInInspector] public GameObject FinalDisplay;
	[HideInInspector] public Image FinalImage;

	private IEnumerator timerCoroutine;
	public GameObject player;
	public GameObject canvasesPrefab;
	public RectTransform stageSelecterPrefab = null;
	public Texture2D[] picList;
	public PlayerStatus playerStatus;

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

//		DontDestroyOnLoad (instance);

//		setupGame ();
	}

	private void OnLevelWasLoaded (int index)
	{
		setupGame ();
	}

	private void setupGame()
	{
		Instantiate(canvasesPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
		
		groundScript = GetComponent<GroundManager> ();
		CameraController = GetComponent<CameraController> ();
		StageSelectCanvas = GameObject.FindGameObjectWithTag ("StageSelectCanvas");
		AndroidInputCanvas = GameObject.FindGameObjectWithTag ("AndroidInputCanvas");
		ProgressSlider = GameObject.FindGameObjectWithTag ("ProgressSlider");
		Timer = GameObject.FindGameObjectWithTag ("Timer");
		FinalDisplay = GameObject.FindGameObjectWithTag ("FinalDisplay");
		FinalImage = GameObject.FindGameObjectWithTag ("FinalImage").GetComponent<Image>();
		StageSelectCanvas.SetActive (true);
		AndroidInputCanvas.SetActive (false);
		FinalDisplay.SetActive (false);
		ProgressSlider.SetActive (false);
		Timer.SetActive (false);
		
		createStageSelecter ();
	}

	private void createStageSelecter()
	{
		for(int i=0; i < picList.Length; i++)
		{
			RectTransform item = GameObject.Instantiate(stageSelecterPrefab) as RectTransform;
			item.GetComponent<StageSelecter>().picData = picList[i];
			
			item.SetParent(GameObject.FindGameObjectWithTag("StageSelectContent").transform, false);
			
			Text text = item.GetComponentInChildren<Text>();
			text.text = "item:" + i.ToString() + " name:" + picList[i].name;
		}
	}

	public void UpdateProgressBar()
	{
		ProgressSlider.GetComponent<Slider>().value = (float)nowCount / (float)targetCount;
	}

	public void InitGame(Texture2D picData)
	{
		groundScript.SetupScene (picData);
		FinalImage.sprite = Sprite.Create(picData,new Rect(0, 0, picData.width, picData.height),Vector2.zero);
		GameObject newPlayer = Instantiate (player, new Vector3(5f,50f,5f), Quaternion.identity) as GameObject;
		newPlayer.GetComponent<PlayerController> ().isMain = true;
		newPlayer.GetComponent<PlayerController> ().playerStatus = SaveDataManager.LoadData<SaveData> ().playerStatus;
		CameraController.InitializePosition(newPlayer);
		StageSelectCanvas.SetActive (false);
		AndroidInputCanvas.SetActive (true);
		ProgressSlider.SetActive (true);
		Timer.SetActive (true);
		timerCoroutine = UpdateTimer();
		StartCoroutine(timerCoroutine);
		SoundManager.instance.PlayGameStart ();
	}
	
	public void CheckIfGameClear()
	{
		if (nowCount >= targetCount) 
		{
			Debug.Log ("GameClear");
			StopCoroutine(timerCoroutine);
			CameraController.ChangeCamera();
			StartCoroutine(SubCamMove(5.0f));
			FinalDisplay.SetActive (true);
			SoundManager.instance.PlayGameOver ();
		}
	}

	IEnumerator SubCamMove(float overTime)
	{
		float t = 0;
		Vector3 source = new Vector3(GameManager.instance.groundScript.columns / 2f, 0f,GameManager.instance.groundScript.rows / 2f);
		Vector3 target = new Vector3(GameManager.instance.groundScript.columns / 2f,(GameManager.instance.groundScript.rows + GameManager.instance.groundScript.columns) / 2f + 40f,GameManager.instance.groundScript.rows / 2f);
		CameraController.mainCam.transform.position = Vector3.zero;
		CameraController.mainCam.transform.LookAt(new Vector3(0f,-1f,0f));

		while(t < 1)
		{
			yield return null;
			t = t + Time.deltaTime / overTime;
			CameraController.mainCam.transform.position = Vector3.Lerp(source, target, t);
		}
		transform.position = target;
	}

	IEnumerator UpdateTimer()
	{
		float StartTime = Time.time;
		float CurrentTime = 0f;
		while(true)
		{
			CurrentTime = Time.time - StartTime;
			Timer.GetComponentInChildren<Text>().text = new TimeSpan(0, 0, (int)CurrentTime).ToString();
			yield return new WaitForSeconds(1);
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveDataTester : MonoBehaviour {

	public GameObject speed;
	public GameObject jumpPower;

	void Start()
	{
		Load ();
	}

	public void Save()
	{
		SaveData data = SaveDataManager.LoadData<SaveData> ();
		data.playerStatus.speed = speed.GetComponent<Slider>().value;
		data.playerStatus.jumpPower = jumpPower.GetComponent<Slider>().value;
		SaveDataManager.SaveData<SaveData> (data);
	}

	public void Load()
	{
		SaveData data = SaveDataManager.LoadData<SaveData> ();
		speed.GetComponent<Slider>().value = data.playerStatus.speed;
		jumpPower.GetComponent<Slider>().value = data.playerStatus.jumpPower;
	}

	public void Reset()
	{
		SaveDataManager.CreateData ();
		Load ();
	}
}

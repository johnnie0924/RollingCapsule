using System;
using System.IO;
using UnityEngine;

public static class SaveDataManager {

	static String savePath;

	static SaveDataManager()
	{
		savePath = Application.persistentDataPath + "/savedata.bin";

		if(!File.Exists(savePath))
		{
			CreateData();
		}
	}

	public static void SaveData<T>(T data)
	{
		XmlUtil.Seialize<T> (savePath, data);
	}

	public static T LoadData<T>()
	{
		return XmlUtil.Deserialize<T> (savePath);
	}

	public static void CreateData()
	{
		SaveData data = new SaveData ();
		data.playerStatus.canChangeSize = false;
		data.playerStatus.canCopyMyself = false;
		data.playerStatus.canSuperMode = false;
		data.playerStatus.jumpPower = 10f;
		data.playerStatus.reSizeDelay = 30f;
		data.playerStatus.speed = 100f;
		data.playerStatus.superModeDelay = 30f;
		data.playerStatus.copyMyselfDelay = 10f;
		data.playerStatus.copyMyselfDelayPerSpawn = 0.5f;
		data.playerStatus.copyMyselfCount = 5;

		Debug.Log (savePath);
		XmlUtil.Seialize<SaveData> (savePath, data);
	}

}

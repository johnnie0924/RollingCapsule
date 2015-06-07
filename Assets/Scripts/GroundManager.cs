using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GroundManager : MonoBehaviour {

	[HideInInspector] public int columns = 1;
	[HideInInspector] public int rows = 1;
	public GameObject groundTile;
	public GameObject[] obstacleTiles;
	public GameObject wallTile;

	public GameObject sphereButton;
	private Hashtable pictureMap;

	private Transform groundHolder;

	void PictureMapSetup(Texture2D picData)
	{
		// Read ImageData from outside
//		Texture2D hogePic = new Texture2D(0,0);
//		debugText.text = Application.dataPath + "/Resources/Texture/m257.PNG";
//		hogePic.LoadImage(LoadBin(Application.dataPath + "/Texture/m257.PNG"));

		// Read ImageData from inside
//		Texture2D hogePic = Resources.Load(picName) as Texture2D;
//		Texture2D hogePic = Resources.Load("Texture/mario") as Texture2D;
		Texture2D hogePic = picData;

		// Set GroundSize
		columns = hogePic.width;
		rows = hogePic.height;

		Debug.Log ("columns:" + columns);
		Debug.Log ("rows" + rows);

		// Set alphaColor(left-top color)
		Color alphaColor = hogePic.GetPixel (0, rows);
		Color tempColor;
		Color leftColor;
		Color rightColor;
		Color upColor;
		Color downColor;

		pictureMap = new Hashtable ();
		for (int i = 0; i < columns; i++) 
		{
			for (int j = 0; j <rows; j++) 
			{
				tempColor = hogePic.GetPixel(i,j);
				leftColor = hogePic.GetPixel(i-1,j);
				rightColor = hogePic.GetPixel(i+1,j);
				upColor = hogePic.GetPixel(i,j-1);
				downColor = hogePic.GetPixel(i,j+1);
				if (ChkIfDrow(tempColor, alphaColor, leftColor, rightColor, upColor, downColor))
				{
					pictureMap.Add(new Vector2 (i, j), hogePic.GetPixel(i,j));
				}
			}
		}
		GameManager.instance.targetCount = pictureMap.Count;
		GameManager.instance.nowCount = 0;
	}

	byte[] LoadBin(string path){
		FileStream fs = new FileStream(path, FileMode.Open);
		BinaryReader br = new BinaryReader(fs);
		byte[] buf = br.ReadBytes( (int)br.BaseStream.Length);
		br.Close();
		return buf;
	}

	void GroundSetup()
	{
		groundHolder = new GameObject ("Ground").transform;

		GameObject instance;

		// Create Wall
		instance = Instantiate(wallTile, new Vector3(columns / 2, wallTile.transform.localScale.y / 2, -1f), Quaternion.identity) as GameObject;
		instance.transform.localScale = new Vector3(columns + 1f, 50f , 1f);
		instance.transform.SetParent(groundHolder);

		instance = Instantiate(wallTile, new Vector3(columns / 2, wallTile.transform.localScale.y / 2, rows), Quaternion.identity) as GameObject;
		instance.transform.localScale = new Vector3(columns + 1f, 50f , 1f);
		instance.transform.SetParent(groundHolder);
		
		instance = Instantiate(wallTile, new Vector3(-1f, wallTile.transform.localScale.y / 2, rows / 2), Quaternion.identity) as GameObject;
		instance.transform.localScale = new Vector3(1f, 50f , rows + 1f);
		instance.transform.SetParent(groundHolder);

		instance = Instantiate(wallTile, new Vector3(columns, wallTile.transform.localScale.y / 2, rows / 2), Quaternion.identity) as GameObject;
		instance.transform.localScale = new Vector3(1f, 50f , rows + 1f);
		instance.transform.SetParent(groundHolder);

		//Create Ground
		instance = Instantiate(groundTile, new Vector3((columns - 1) / 2f, 0f, (rows - 1) / 2f), Quaternion.identity) as GameObject;
		instance.transform.localScale = new Vector3(columns, 1f , rows);
		instance.transform.SetParent(groundHolder);

		//Create Obstacle & Button
		for(int i = 0; i < columns; i++)
		{
			for(int j = 0; j < rows; j++)
			{
				GameObject toInstantinate;
				float height = 1f; 
				toInstantinate = obstacleTiles[Random.Range(0, obstacleTiles.Length)];

				if (toInstantinate != null)
				{
					instance = Instantiate(toInstantinate, new Vector3(i,toInstantinate.transform.localScale.y / 2,j), Quaternion.identity) as GameObject;
					height = toInstantinate.transform.localScale.y;
					instance.transform.SetParent(groundHolder);
				}

				if (pictureMap.ContainsKey(new Vector2(i,j)))
				{
					instance = Instantiate(sphereButton,new Vector3(i,height,j),Quaternion.identity) as GameObject;
					instance.GetComponent<ButtonStatus>().myColor = (Color) pictureMap[new Vector2(i,j)];
					instance.transform.SetParent(groundHolder);
				}
			}
		}
		pictureMap.Clear ();
	}

	public void SetupScene(Texture2D picData)
	{
		PictureMapSetup (picData);
		GroundSetup ();
		Debug.Log ("GroundSetup Start");
	}

	private bool ChkIfDrow(Color temp, Color alpha, Color left, Color right, Color up, Color down)
	{
		if (temp != Color.white && temp != alpha && !(temp == left && temp == right && temp == up && temp == down)) 
		{
			return true;
		}
		else 
		{
			return false;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GroundManager : MonoBehaviour {

	[HideInInspector] public int columns = 1;
	[HideInInspector] public int rows = 1;
	public GameObject groundTile;
	public GameObject wallTile;

	public GameObject sphereButton;
	private Hashtable pictureMap;

	void PictureMapSetup(Texture2D picData)
	{
		// Set GroundSize
		columns = picData.width;
		rows = picData.height;

//		Debug.Log ("columns:" + columns);
//		Debug.Log ("rows" + rows);

		// Set alphaColor(left-top color)
		Color alphaColor = picData.GetPixel (0, rows);
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
				tempColor = picData.GetPixel(i,j);
				leftColor = picData.GetPixel(i-1,j);
				rightColor = picData.GetPixel(i+1,j);
				upColor = picData.GetPixel(i,j-1);
				downColor = picData.GetPixel(i,j+1);
				if (ChkIfDrow(tempColor, alphaColor, leftColor, rightColor, upColor, downColor))
				{
					pictureMap.Add(new Vector2 (i, j), picData.GetPixel(i,j));
				}
			}
		}
		GameManager.instance.targetCount = pictureMap.Count;
		GameManager.instance.nowCount = 0;
	}

	void GroundSetup()
	{
		Transform groundHolder = new GameObject ("Ground").transform;

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
				if (pictureMap.ContainsKey(new Vector2(i,j)))
				{
					instance = Instantiate(sphereButton,new Vector3(i,1f,j),Quaternion.identity) as GameObject;
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

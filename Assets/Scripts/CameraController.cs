using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector3 originalPosition;
	private Vector3 distanceFromPlayer;
	[HideInInspector] public GameObject player = null;

	[HideInInspector] public GameObject mainCam;
	[HideInInspector] public GameObject subCam;
	private bool usingMainCam;

	void Awake () {
		usingMainCam = true;
		mainCam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
		subCam = GameObject.FindGameObjectsWithTag("SubCamera")[0];
		subCam.SetActive (false);
		
		originalPosition = mainCam.transform.position;
		distanceFromPlayer = originalPosition;
	}

	void Update () {
		if (usingMainCam) 
		{
			if (player == null) 
			{
				mainCam.transform.position = originalPosition;
			}
			else 
			{
				mainCam.transform.position = player.transform.position + distanceFromPlayer;
			}
		}
	}

	public void InitializePosition(GameObject newPlayer)
	{
		player = newPlayer;
	}

	public void ChangeCamera()
	{
		if (usingMainCam) 
		{
			mainCam.SetActive (false);
			subCam.SetActive (true);
			usingMainCam = false;
		}
		else 
		{
			mainCam.SetActive (true);
			subCam.SetActive (false);
			usingMainCam = true;
		}
	}
}

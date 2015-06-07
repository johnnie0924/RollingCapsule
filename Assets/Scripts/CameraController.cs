using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector3 originalPosition;
	private Vector3 distanceFromPlayer;
	[HideInInspector] public GameObject player = null;

	[HideInInspector] public GameObject mainCam;
	private bool usingMainCam;

	void Awake () {
		setupCamera ();
	}

	void Update () {
		if (usingMainCam && mainCam != null) 
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
		setupCamera ();
	}

	void setupCamera()
	{
		usingMainCam = true;
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		
		originalPosition = mainCam.transform.position;
		distanceFromPlayer = originalPosition;
	}

	public void ChangeCamera()
	{
		if (usingMainCam) 
		{
			usingMainCam = false;
		}
		else 
		{
			usingMainCam = true;
		}
	}
}

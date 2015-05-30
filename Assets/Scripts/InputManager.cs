using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	void Update () {

		if (Input.GetKey(KeyCode.Z))
		{
			GameManager.instance.pressZ = true;
		}

		if (Input.GetKey(KeyCode.X))
		{
			GameManager.instance.pressX = true;
		}
		
		if (Input.GetKey(KeyCode.C))
		{
			GameManager.instance.pressC = true;
		}
		
		if (Input.GetKey(KeyCode.V))
		{
			GameManager.instance.pressV = true;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			GameManager.instance.pressSpace = true;
		}
	}
	
	public void PressZ()
	{
		GameManager.instance.pressZ = true;
	}

	public void PressX()
	{
		GameManager.instance.pressX = true;
	}

	public void PressC()
	{
		GameManager.instance.pressC = true;
	}

	public void PressV()
	{
		GameManager.instance.pressV = true;
	}
	
	public void PressSpace()
	{
		GameManager.instance.pressSpace = true;
	}
}
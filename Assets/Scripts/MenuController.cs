using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public void loadTop()
	{
		Application.LoadLevel("Top");
	}

	public void loadStoryMode()
	{
		Application.LoadLevel("Main");
	}

	public void loadCollection()
	{

	}

	public void loadOption()
	{
		Application.LoadLevel("CharaCustom");
	}

	public void loadHelp()
	{
		Application.LoadLevel("Help");
	}

}

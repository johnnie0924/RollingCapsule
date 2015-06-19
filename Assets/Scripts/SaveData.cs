using System.Collections;
using System.Text;

public class SaveData {

	public PlayerStatus playerStatus;

	public SaveData()
	{
		playerStatus = new PlayerStatus ();
	}

	override public string ToString()
	{
		return playerStatus.ToString ();
	}
}

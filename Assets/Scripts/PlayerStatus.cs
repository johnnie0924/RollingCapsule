using System.Collections;
using System.Text;

[System.Serializable]
public class PlayerStatus{

	//basis
	public float speed;
	public float jumpPower;

	//technique changeSize
	public bool canChangeSize;
	public float reSizeDelay;

	//technique superMode 
	public bool canSuperMode;
	public float superModeDelay;

	//technique copyMyself
	public bool canCopyMyself;
	public float copyMyselfDelay;
	public float copyMyselfDelayPerSpawn;
	public int copyMyselfCount;

	override public string ToString()
	{
		StringBuilder str = new StringBuilder ();

		str.Append ("---PlayerStatus---");
		str.AppendLine ();

		str.Append ("<basis>");
		str.AppendLine ();
		str.Append ("speed : ");
		str.Append (speed);
		str.AppendLine ();
		str.Append ("jumpPower : ");
		str.Append (jumpPower);
		str.AppendLine ();
		
		str.Append ("<changeSize>");
		str.AppendLine ();
		str.Append ("canChangeSize : ");
		str.Append (canChangeSize);
		str.AppendLine ();
		str.Append ("reSizeDelay : ");
		str.Append (reSizeDelay);
		str.AppendLine ();

		str.Append ("<superMode>");
		str.AppendLine ();
		str.Append ("canSuperMode : ");
		str.Append (canSuperMode);
		str.AppendLine ();
		str.Append ("superModeDelay : ");
		str.Append (superModeDelay);
		str.AppendLine ();

		str.Append ("<copyMyself>");
		str.AppendLine ();
		str.Append ("canCopyMyself : ");
		str.Append (canCopyMyself);
		str.AppendLine ();
		str.Append ("copyMyselfDelay : ");
		str.Append (copyMyselfDelay);
		str.AppendLine ();
		str.Append ("copyMyselfDelayPerSpawn : ");
		str.Append (copyMyselfDelayPerSpawn);
		str.AppendLine ();
		str.Append ("copyMyselfCount : ");
		str.Append (copyMyselfCount);
		str.AppendLine ();
		return str.ToString();
	}
}

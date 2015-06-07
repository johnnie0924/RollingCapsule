using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public float jumpPower;

	public float reSizeDelay = 100f;
	public float superModeDelay = 100f;
	public float copyMyselfDelay = 10f;
	public float copyMyselfDelayPerSpawn = 0.5f;
	public int copyMyselfCount = 5;

	private bool isJumping;
	private bool isChangeSize;
	private bool isSuperMode;
	private bool isCopyMyself;

	public bool isMain;

	void Awake () 
	{
		Input.gyro.enabled = true;
		isJumping = false;
		isChangeSize = false;
		isSuperMode = false;
		isCopyMyself = false;
		isMain = false;
	}
	
	void FixedUpdate () 
	{
		Vector3 movement;
		
		#if UNITY_ANDROID
		// ジャイロから重力の下向きのベクトルを取得
		Vector3 gravityV = Input.gyro.gravity;
		
		// 外力のベクトルを計算.
		float scale = 10.0f;
		movement = new Vector3(gravityV.x, 0.0f, gravityV.y) * scale;
		
		// jump判定
		if(Input.gyro.userAcceleration.z > 0.2f)
		{
			GameManager.instance.pressSpace = true;
		}
		#endif
		
		#if UNITY_EDITOR
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		
		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		#endif

		// jump判定
		if (GameManager.instance.pressSpace)
		{
			GameManager.instance.pressSpace = false;
			if(!isJumping)
			{
				jump ();
			}
		}

		// ChangeSize判定
		if (GameManager.instance.pressX)
		{
			GameManager.instance.pressX = false;
			if(!isChangeSize)
			{
				StartCoroutine(ChangeSize ());
			}
		}

		// SuperMode判定
		if (GameManager.instance.pressC)
		{
			GameManager.instance.pressC = false;
			if(!isSuperMode)
			{
				StartCoroutine(ChangeSuperMode ());
			}
		}

		// CopyMyself判定
		if (GameManager.instance.pressV)
		{
			GameManager.instance.pressV = false;
			if(!isCopyMyself && isMain)
			{
				StartCoroutine(CopyMyself ());
			}
		}

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.tag == "Button")
		{
			if(!other.gameObject.GetComponent<ButtonStatus>().beColored)
			{
				other.gameObject.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<ButtonStatus>().myColor;
				other.gameObject.GetComponent<ButtonStatus>().beColored = true;
				other.gameObject.GetComponent<Collider>().enabled = false;
				SoundManager.instance.PlayConflict();
				GameManager.instance.nowCount++;
				GameManager.instance.UpdateProgressBar();
				GameManager.instance.CheckIfGameClear();
				if(!isSuperMode)
				{
					Vector3 bounceVec3 = new Vector3 (-GetComponent<Rigidbody>().velocity.x, -GetComponent<Rigidbody>().velocity.y, -GetComponent<Rigidbody>().velocity.z);
					float dis = Vector3.Distance(bounceVec3,Vector3.zero);
					if (dis == 0f) dis = float.Epsilon;
					GetComponent<Rigidbody>().AddForce (bounceVec3 / dis * 500.0f);
				}
			}
			isJumping = false;
		}
		else if (other.gameObject.tag == "Ground") 
		{
			isJumping = false;
		}
	}
	
	private void jump()
	{
		isJumping = true;
		SoundManager.instance.PlayJump ();
		GetComponent<Rigidbody>().AddForce (new Vector3 (0.0f, 1.0f, 0.0f) * speed * jumpPower * Time.deltaTime);
	}

	IEnumerator ChangeSize()
	{
		isChangeSize = true;
		Vector3 originalSize = transform.localScale;
		transform.localScale = originalSize * 3;
		yield return new WaitForSeconds (reSizeDelay);
		transform.localScale = originalSize;
		isChangeSize = false;
	}

	IEnumerator ChangeSuperMode()
	{
		isSuperMode = true;
		Color original = transform.GetComponent<Renderer> ().material.color;
		transform.GetComponent<Renderer> ().material.color = Color.yellow;
		yield return new WaitForSeconds (superModeDelay);
		transform.GetComponent<Renderer> ().material.color = original;
		isSuperMode = false;
	}

	IEnumerator CopyMyself()
	{
		isCopyMyself = true;
		GameObject[] copies = new GameObject[copyMyselfCount]; 
		for(int i = 0;i < copyMyselfCount; i++)
		{
			copies[i] = Instantiate(GameManager.instance.player,transform.localPosition,Quaternion.identity) as GameObject;
			copies[i].GetComponent<Rigidbody>().AddForce(new Vector3 (0.0f, 1.0f, 0.0f));
			yield return new WaitForSeconds (copyMyselfDelayPerSpawn);
		}
		yield return new WaitForSeconds (copyMyselfDelay);
		for (int i = 0; i < copyMyselfCount; i++) 
		{
			Destroy (copies [i]);
			yield return new WaitForSeconds (copyMyselfDelayPerSpawn);
		}
		isCopyMyself = false;
	}
}
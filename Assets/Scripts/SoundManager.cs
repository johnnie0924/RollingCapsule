using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource conflictSource;
	public AudioSource jumpSource;
	public AudioSource gameStartSource;
	public AudioSource gameOverSource;
	public AudioSource stageBGMSource;

	void Awake () 
	{
		if (instance == null) 
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad (gameObject);
	}

	public void startStageBGM ()
	{
		stageBGMSource.Play ();
	}

	public void stopStageBGM ()
	{
		stageBGMSource.Stop ();
	}

	public void PlayGameStart ()
	{
		gameStartSource.Play ();
	}

	public void PlayGameOver ()
	{
		gameOverSource.Play ();
	}

	public void PlayConflict ()
	{
		conflictSource.Play ();
	}

	public void PlayJump ()
	{
		jumpSource.Play ();
	}
}

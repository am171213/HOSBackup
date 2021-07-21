using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
	public static bool gamePaused = false;
	public GameObject pauseMenuUI;

	public GameObject pauseFirstButton, resumeButton, menuButton, optionsButton, quitButton;

	void Awake()
	{
		gamePaused = false;
	}
		
    // Update is called once per frame
    void Update()
	{
		if (Input.GetButtonDown("Pause"))
			{
				if (gamePaused)
				{
					Resume();
				}
				else{Pause();}
			}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		gamePaused = false;
	}

	public void Pause()
	{
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		gamePaused = true;
	}

	public bool GetIsPaused()
	{
		return gamePaused;
	}

	public void MainMenu()
	{
		SceneManager.LoadScene ("Menu");
	}
	public void Options()
	{
		Debug.Log ("Options");
	}

	public void Quit()
	{
		Debug.Log("Quit");
		Application.Quit ();
	}
}

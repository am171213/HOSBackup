using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	private GameMaster gm;
	private PlayerController ed;

	void Start ()
	{
		gm = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		//ed = GameObject.Find("Edward").GetComponent<PlayerController>();
	}

	public void Play()
	{
		SceneManager.LoadScene ("Floor1");
		Time.timeScale = 1f;
	}

	public void Quit()
	{
		Application.Quit ();
	}

}

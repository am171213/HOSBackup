using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	private GameMaster gm;

	void Start ()
	{
		gm = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
	}

	public void Play()
	{
		SceneManager.LoadScene ("Floor1");
	}

	public void Quit()
	{
		Application.Quit ();
	}

}

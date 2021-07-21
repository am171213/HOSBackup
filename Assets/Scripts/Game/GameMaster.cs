using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameMaster : MonoBehaviour
{
	private static GameMaster masterInstance;
	private static int difficultyChosen = 0;

	public void Awake()
	{
		DontDestroyOnLoad (this);
		if (masterInstance == null) {
			masterInstance = this;
		} else {
			Destroy (gameObject);
		}		
	}
	public void SetDifficulty(int diff)
	{
		difficultyChosen = diff;
	}

	public int GetDifficulty()
	{
		return difficultyChosen;
	}
	
	void Update()
	{
		Scene currScene = SceneManager.GetActiveScene();
		
		if (currScene.name != "Menu")
		{
			GetComponent<Events>().enabled = true;
		}
	}
}

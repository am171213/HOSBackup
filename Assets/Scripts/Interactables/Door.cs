using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
	public GameObject entryPoint;
	private CameraController cameraAccess;
	private GameObject game;
	private GameMaster gameMaster;
	public Level newCamData;
	public bool locked = true;

	public override void Start()
	{
		player = FindObjectOfType<PlayerController>();
		objCanvasText = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
		game = GameObject.Find ("GameMaster");
		gameMaster = game.GetComponent<GameMaster> ();
		cameraAccess = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
	}
	
	public override void Interaction()
	{
		if (locked == false)
		{
			DoorOpen();
		}
		if (locked == true)
		{
			base.Interaction();
		}
	}
 
    public void DoorOpen()
	{
		if (locked == false)
		{
			player.transform.position = new Vector3(entryPoint.transform.position.x, entryPoint.transform.position.y, player.transform.position.z);
			cameraAccess.RoomChange(newCamData);
			triggered = true;
		}
	}

	public void Unlock()
	{
		locked = false;
		//Debug.Log("Door is unlocked");
	}
}

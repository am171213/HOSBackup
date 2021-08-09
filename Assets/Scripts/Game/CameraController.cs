using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	//Player position
	private GameObject player;
	private Vector3 playerPosition;
	private float minX, maxX, xOffset;
	private float camX;
	private float camY;
	private float playerX;
	public Level camData;

	void Start()
	{
		player = GameObject.Find ("Edward");
		InitialSetup();
	}
	
	void InitialSetup()
	{
		camX = camData.startCamX;
		camY = camData.startCamY;
		xOffset = camData.camXOffset;
		ChangeCamPosition(camX, camY);
		minX = camData.minCamX;
		maxX = camData.maxCamX;
		
	}


    void Update()
    {
		//Constantly Updating New Player Position
		playerX = player.transform.position.x;	
		camX = playerX;
		CheckCameraBounds();
    }
		
	//Check if the camera position is out of bounds of the level
	void CheckCameraBounds()
	{
		if (camX > maxX)
		{	
			camX = maxX;
			ChangeCamPosition(maxX,camY);
		}
		else if (camX < minX)
		{
			camX = minX;
			ChangeCamPosition(minX,camY);
		}
		else
		{
			ChangeCamPosition(camX+xOffset,camY);
		}
	}

	public void RoomChange(Level newCamData)
	{
		camData = newCamData;
		camX = camData.startCamX;
		minX = camData.minCamX;
		maxX = camData.maxCamX;
		camY = camData.camYPos;
		xOffset = camData.camXOffset;
		//Debug.Log(camX);
		//Debug.Log(xOffset);
		ChangeCamPosition(camX+xOffset,camY);
	}
	
	public void ChangeCamPosition(float xPos, float yPos)
	{
		//Debug.Log(xPos);
		gameObject.transform.position = new Vector3(xPos, yPos, gameObject.transform.position.z);
	}
}

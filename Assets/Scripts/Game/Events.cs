using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Events : MonoBehaviour
{
	//Game Objects
	public GameObject globalLight;
	new private CameraController camera;
	public GameObject player;
	public GameObject triggerDesk;
	private GameObject lightSwitch;
	private EmLightSwitch emLightCont;
	public Level event1CamData;
	public Level cutscene1CamData;
	public GameObject triggerDoor;
	
	//Event Booleans
	private bool event1Start = false;
	private bool cutscene1Start = false;
	//public bool event2Start = false;
	//public bool event3Start = false;
	
	//Event Variables To Be Made Into Constants Later
	public float waitTime;
	public float loadTime;	
	
	public void Start()
	{
		//Commonly Used Objects
		camera = GameObject.Find("Main Camera").GetComponent<CameraController> ();	
		lightSwitch = GameObject.Find("EmLightSwitch");
		emLightCont = lightSwitch.GetComponent<EmLightSwitch>();
	}
	
	public void Update()
	{
		if(lightSwitch != null)
		{emLightCont = lightSwitch.GetComponent<EmLightSwitch>();}
		
		if (player != null && lightSwitch != null)
		{
			if(!event1Start)
			{
				EventHandler();	
				CoroutineHandler();
			}
			if(!cutscene1Start)
			{
				EventHandler();	
				CoroutineHandler();
			}
		}
		
		
	}
	
	public void EventHandler()
	{
		if (triggerDesk.GetComponent<Desk>().GetTriggered() && triggerDesk != null && !event1Start)
		{event1Start = true;}
		
		if (triggerDoor.GetComponent<Door>().GetTriggered() && triggerDoor != null && !cutscene1Start)
		{
			cutscene1Start = true;
		}
	}
	
	public void CoroutineHandler()
	{
		if (event1Start)
		{
			StartCoroutine(Floor1RoomChange(waitTime, loadTime));
		}
		if (cutscene1Start)
		{
			StartCoroutine(Cutscene1());
		}
	}
	
	public IEnumerator Floor1RoomChange(float waitT, float loadT)
	{
		yield return new WaitForSeconds(waitTime);
		globalLight.GetComponent<Animator>().SetBool("LightsOff", true);
		yield return new WaitForSeconds(loadTime);
		
		player.GetComponent<PlayerController>().playerSpeed = 0.0f;
				
		event1CamData.startCamX = player.transform.position.x;
		camera.RoomChange(event1CamData);
		
		if (player != null)
		{player.transform.position = new Vector3(player.transform.position.x, -16.35059f, 0.0f);}
		
		if (emLightCont != null)
		{emLightCont.TurnOnLights();}
		
		player.GetComponent<PlayerController>().playerSpeed = 2.0f;
		player.GetComponent<PlayerController>().sprinting = false;
		
		triggerDesk.GetComponent<Desk>().SetTriggered(false);
		event1Start = false;
		//Debug.Log("Room Change");
	}
	
	public IEnumerator Cutscene1 ()
	{	
		//Debug.Log("Play Cutscene");
		player.GetComponent<PlayerController>().playerSpeed = 0.0f;
		camera.RoomChange(cutscene1CamData);
	
		yield return new WaitForSeconds(1);	
		cutscene1Start = false;
	}
}

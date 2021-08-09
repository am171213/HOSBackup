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
	public GameObject lucyObject;
	public bool inCutscene = false;
	public bool playText = false;
	
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
		Debug.Log("Start");
		
		//Commonly Used Objects
		camera = GameObject.Find("Main Camera").GetComponent<CameraController> ();	
		lightSwitch = GameObject.Find("EmLightSwitch");
		emLightCont = lightSwitch.GetComponent<EmLightSwitch>();
		triggerDesk = GameObject.Find("TriggerDesk");
		triggerDoor =  GameObject.Find("TriggerDoor");
		globalLight = GameObject.Find("GlobalLight");
		player = GameObject.Find("Edward");
		lucyObject = GameObject.Find("Lucy");
		
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
		{event1Start = true;
			Debug.Log("Start RoomChange");
		}
		
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
		yield return new WaitForSecondsRealtime(waitTime);
		globalLight.GetComponent<Animator>().SetBool("LightsOff", true);
		yield return new WaitForSecondsRealtime(loadTime);
				
		event1CamData.startCamX = player.transform.position.x;
		camera.RoomChange(event1CamData);
		
		if (player != null)
		{player.transform.position = new Vector3(player.transform.position.x, -16.35059f, 0.0f);}
		
		if (emLightCont != null)
		{emLightCont.TurnOnLights();}
		
		triggerDesk.GetComponent<Desk>().SetTriggered(false);
		event1Start = false;
	}
	
	public IEnumerator Cutscene1 ()
	{		
		inCutscene = true;
		//Debug.Log("Play Cutscene");
		//player.GetComponent<PlayerController>().playerSpeed = 0.0f;
		camera.RoomChange(cutscene1CamData);
	
		if(!playText)
		{
			player.GetComponent<PlayerController>().DialogueEvent(new string[] {"Lucy!","Oh God. No!"});
			
			playText = true;
		}
		
		lucyObject.GetComponent<Lucy>().lucyTransform = true;
		yield return new WaitForSeconds(5);
		
		lucyObject.GetComponent<Lucy>().lucyDisappear = true;
		yield return new WaitForSeconds(4);	
		
		inCutscene = false;	
		cutscene1Start = false;
	}
}

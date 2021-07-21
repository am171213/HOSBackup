using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : Interactable
{
	public GameObject matchingDoor;
	public bool activated = false;
	private Animator anim;

	
	public override void Start()
	{
		anim = gameObject.GetComponent<Animator>();
		player = FindObjectOfType<PlayerController>();
		objCanvasText = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
	}
	
	public override void Interaction()
	{
		if (activated)
		{
			matchingDoor.GetComponent<Door>().Unlock();
			if (hasAltText && activated)
			{
				dialogue.SwitchText(true);
			}
		}
		
		TriggerDialogue();
	}
	
	public IEnumerator KeycardAnim()
	{
		if (activated)
		{
			
			//Debug.Log("Green Light");
			anim.SetInteger("State",1);
			yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		}
		else
		{

			//Debug.Log("Red Light");
			anim.SetInteger("State",-1);			
			yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		}
		
		//Debug.Log("No Light");
		anim.SetInteger("State",0);
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
		
	}
}


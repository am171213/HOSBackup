using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
	public Dialogue dialogue;
	protected PlayerController player;
	protected GameObject objCanvasText;
	protected bool triggered;
	public bool hasAltText = false;
	[SerializeField]
	public string matchingItemCode;
	public bool hasMatchingItem = false;

	
	public virtual void Start()
	{
		player = FindObjectOfType<PlayerController>();
		objCanvasText = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
	}
	
	public virtual void Interaction()
	{
		TriggerDialogue();
		if (hasAltText)
		{
			dialogue.SwitchText(true);
		}
	}
	
	public string GetObjectCode()
	{
		return matchingItemCode;
	}
	
	public bool UseItemOn(string itemCode)
	{	
		//Debug.Log("Item being used: " + item);
		//Debug.Log("Item is not null.");
		if (itemCode == matchingItemCode)
		{
			return true;
		}
		else
		{
			//Debug.Log("Item codes don't match");
			//Debug.Log("The items code: " + itemCode);
			//Debug.Log("The objects code: " + matchingItemCode);
			return false;
		}
	}

	public virtual void TriggerDialogue()
	{FindObjectOfType<DialogueController>().StartDialogue(dialogue);}
	
	public void DisplayMessage()
	{objCanvasText.GetComponent<Text>().text = dialogue.instructText;}
	
	public void ClearMessage()
	{objCanvasText.GetComponent<Text>().text = "";}
	
	public bool GetTriggered(){return triggered;}
	
	public void SetTriggered(bool trig)
	{triggered = trig;}
}	

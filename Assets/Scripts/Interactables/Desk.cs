using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : Interactable
{
	
	public override void TriggerDialogue()
	{
		triggered = true;
		FindObjectOfType<DialogueController>().StartDialogue(dialogue);
	}
}

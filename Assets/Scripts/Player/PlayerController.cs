/*
Edward Control Script:
-Health System
-Inventory
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController: MonoBehaviour
{
	//-------------------------------------------PRIVATE VARIABLES------------------------------------------//
	//Components
	private SpriteRenderer spriteRend;
	private Rigidbody2D rb2d;
	private Animator anim;

	//Game Objects
	private GameObject interactingObject;
	private GameObject itemObject;
	private GameObject dialogueManager;
	private InventoryController inventoryController;
	public Dialogue dialogue;
	public Queue<string> edwardText;
	private Events eventManager;

	//Movement
	public float playerSpeed = 2;
	private const float RUN_SPEED = 4;
	private const float WALK_SPEED = 2;
	public float movementInput;

	//Booleans
	public bool sprinting = false;
	private bool lightOut = false;
	private bool flipped = false;
	private bool onInteractable = false;
	public bool interacting = false;
	private bool showInventory = false;
	private bool onItem = false;
	
	//UI
	public string instructionText;

	//-------------------------------------------PUBLIC VARIABLES------------------------------------------//

	//-------------------------------------------SYSTEM FUNCTIONS------------------------------------------//

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		spriteRend = GetComponent<SpriteRenderer> ();
		//audioControl = GetComponent<AudioSource> ();
		//lightSourceObject = gameObject.transform.GetChild (0).gameObject;
		dialogueManager = GameObject.Find("DialogueManager");
		inventoryController = GameObject.Find("InventoryManager").GetComponent<InventoryController>();
		eventManager = GameObject.Find("GameMaster").GetComponent<Events>();
	}
		
	void Update()
	{
		
		
		if (!eventManager.inCutscene)
		{
			//Movement Left or Right
			movementInput = Input.GetAxisRaw("Horizontal");
			//Sprint Button
			if (Input.GetButtonDown ("Sprint")) 
			{
				Sprint ();
			}

			//Interact Button
			if (Input.GetButtonDown ("Interact"))	
			{
				if (onInteractable)
				{
					if (!interacting && interactingObject!= null)
					{
						interacting = true;
						//Debug.Log(interactingObject);
						interactingObject.GetComponent<Interactable>().Interaction();	
					}
					else 
					{
						dialogueManager.GetComponent<DialogueController>().DisplayNextSentence();
					}
				}
				else
				{
					dialogueManager.GetComponent<DialogueController>().EndDialogue();
				}
			}
			else
			{			
				if (onInteractable && interactingObject != null)
				{
					interactingObject.GetComponent<Interactable>().DisplayMessage();
				}
			}
			
			if(Input.GetButtonDown ("Inventory"))
			{
				if(!showInventory)
				{
					showInventory = true;
					inventoryController.OpenInventory();
				}
				
				else if(showInventory)
				{
					showInventory = false;
					inventoryController.CloseInventory();
				}
			}
			
			if (Input.GetButtonDown("PickUp") && onItem)
			{
				if (itemObject != null )
				{
					inventoryController.AddToInventory(itemObject);
					Destroy(itemObject);
				}
			}
			
			var input = Input.inputString;
			
			if (showInventory && onInteractable)
			{
				if (input == "1" ||input == "2" ||input == "3" ||input == "4" || input =="5" || input =="6" || input =="7" ||input == "8" ||input == "9" || input =="0")
				{
					int itemNum = Convert.ToInt32(input);
					if (inventoryController.IsItemValid(itemNum))
					{
						if (inventoryController.CheckItemMatching(itemNum, interactingObject))
						{
							Item item = inventoryController.GetItem(itemNum);
							if(interactingObject.GetComponent<Interactable>().UseItemOn(inventoryController.GetItemSlot(itemNum).GetComponent<Item>().GetItemCode()))
							{
								if (interactingObject.GetComponent<Keycard>() != null)
								{	
									interactingObject.GetComponent<Keycard>().activated = true;
									StartCoroutine(interactingObject.GetComponent<Keycard>().KeycardAnim());
									//Debug.Log("Interacting object has keycard component.");
									DialogueEvent(new string[] {"The door is unlocked."});
								}
								
								//interactingObject.GetComponent<Interactable>().UseItemOn
								if (item.itemRemoval == true)
								{
									inventoryController.RemoveFromInventory(itemNum);
								}	
							}
						}
						else
						{	
							if (interactingObject.GetComponent<Keycard>() != null)
							{
								StartCoroutine(interactingObject.GetComponent<Keycard>().KeycardAnim());
							}
							DialogueEvent(new string[] {"That doesn't go there."});
							
						}
					}
				}
			}	
			input = "";
		}
		
		else
		{
			Debug.Log("In Cutscene");
			if (Input.GetButtonDown ("Interact"))
			{
				dialogueManager.GetComponent<DialogueController>().DisplayNextSentence();
			}
		}
	}

	private void FixedUpdate()
	{
		Vector2 movement = new Vector2(movementInput * playerSpeed, rb2d.velocity.y);//Move the player
		rb2d.velocity = movement;
		AnimationHandler (); 		//Control Animation States
		FlipSprite ();						//Flip the Player
	}
	
	public void DialogueEvent(string[] textToSay)
	{
		dialogue.textToSay = textToSay;
		//Debug.Log(dialogue.textToSay[0]);
		dialogueManager.GetComponent<DialogueController>().SetTextToSay(dialogue.textToSay);
		//Debug.Log(dialogue.textToSay[0]);
		dialogueManager.GetComponent<DialogueController>().StartDialogue(dialogue);
		//Debug.Log(dialogue.textToSay[0]);
	}

	//-------------------------------------------CONTROLS------------------------------------------//
	void Sprint()
	{
		//If not sprinting then sprinting is true
		if (!sprinting) {
			playerSpeed = RUN_SPEED;
			sprinting = true;
		} 

		//If sprinting then sprinting is false
		else {
			playerSpeed = WALK_SPEED;
			sprinting = false;
		}
	}

	void Flashlight()
	{
		//If light is not out then light is on and animated.
		if (!lightOut) {
			lightOut = true;
			//lightSourceObject.SetActive (true);
			anim.SetBool ("lightOut", true);
		} 
		//If light is out then light is off and hidden.
		else {
			lightOut = false;
			//lightSourceObject.SetActive (false);
			anim.SetBool ("lightOut", false);
		}
	}

	//-------------------------------------------UI FUNCTIONS------------------------------------------//

	//-------------------------------------------ANIMATIONS------------------------------------------//

	void AnimationHandler ()
	{
		anim.SetBool ("isRunning", sprinting);
		anim.SetFloat ("PlayerSpeed", Mathf.Abs(movementInput * playerSpeed));
	}

	void FlipSprite()
	{
		//If movement isn't happpening then don't play walking sound.
		if (movementInput == 0) {
			//IdleSound ();
		}

		//If movement is happening to the left then flip the sprite and if the flashlight is out, flip that too.
		//Play walking sound
		else if (movementInput < 0) 
		{
			spriteRend.flipX = true;
			WalkingSound ();
			if (!flipped) 
			{
				flipped = true;
			}
		} 

		//If movement is happening to the right then flip the sprite back and if the flashlight is out flip that back too.
		//Play walking sound
		else if (movementInput > 0) 
		{
			spriteRend.flipX = false;
		}
	}

	//-------------------------------------------SOUNDS------------------------------------------//

	void WalkingSound ()
	{
		//audioControl.Play (0);
	}
		
	void IdleSound()
	{
		//audioControl.Pause ();
	}

	//-------------------------------------------ON TRIGGERS------------------------------------------//
	
	void OnTriggerStay2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "Interactables" || collision.gameObject.tag == "Item")
		{
			if (collision.gameObject.tag == "Interactables")
			{
				interactingObject = collision.gameObject;	
				onInteractable = true;
			}
			else if (collision.gameObject.tag == "Item")
			{
				itemObject = collision.gameObject;
				onItem = true;
			}
		}
		else
		{
			interactingObject = null;
		}
	}
	

	//If the player stops overlapping a trigger object this function is called
	//If the object the player started overlapping is the same as the object the player has stopped overlapping then potentialInteract = false
	//Because the player is no longer overlapping that object
	void OnTriggerExit2D (Collider2D collision)
	{
		//Debug.Log("Exitting: " + interactingObject);

		if(interactingObject != null)
		{
			if (interactingObject.GetComponent<Door>() != null && !onInteractable)
			{
				interacting = false;
				onInteractable = true; 
				interactingObject.GetComponent<Interactable>().ClearMessage();
				dialogueManager.GetComponent<DialogueController>().EndDialogue();
			}
			else
			{
				interactingObject.GetComponent<Interactable>().ClearMessage();
				interacting = false;
				onInteractable = false;
				dialogueManager.GetComponent<DialogueController>().EndDialogue();
			}
			
		}
		if(itemObject != null)
		{
			itemObject = null;
			interacting = false;
			if (interactingObject != null)
			{
				interactingObject.GetComponent<Interactable>().ClearMessage();
			}
		}
	}

	//-------------------------------------------GETTERS------------------------------------------//
	
	public bool GetSprinting()
	{
		return sprinting;
	}
	
	public float GetMovementInput()
	{
		return movementInput;
	}
	
	public float GetPlayerSpeed()
	{
		return playerSpeed;
	}
	
	//-------------------------------------------SETTERS------------------------------------------//
	
	public void SetInteracting (bool newInteracting)
	{
		interacting = newInteracting;
	}
	
	//--------------------------------------------COROUTINES--------------------------------------//
	
	
	
}

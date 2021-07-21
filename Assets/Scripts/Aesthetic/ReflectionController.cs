using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController : MonoBehaviour
{
	private Animator mirrorAnim;
	private GameObject edward;
	private bool sprinting;
	private float movementInput;
	private float playerSpeed;
	
    void Start()
    {
	   mirrorAnim = this.GetComponent<Animator>();
	   edward = this.transform.parent.gameObject;
	   sprinting = edward.GetComponent<PlayerController>().GetSprinting();
	   movementInput = edward.GetComponent<PlayerController>().GetMovementInput();
	   playerSpeed = edward.GetComponent<PlayerController>().GetPlayerSpeed();
    }

    void Update()
    {
		UpdateValues();
		ReflectionAnimationHandler();
        if (edward.GetComponent<SpriteRenderer>().flipX == true)
		{
			this.GetComponent<SpriteRenderer>().flipX = true;
		}
		else
		{
			this.GetComponent<SpriteRenderer>().flipX = false;
		}
    }
	
	void ReflectionAnimationHandler ()
	{
		mirrorAnim.SetBool ("isRunning", sprinting);
		mirrorAnim.SetFloat ("PlayerSpeed", Mathf.Abs(movementInput * playerSpeed));
	}
	
	void UpdateValues()
	{
	   sprinting = edward.GetComponent<PlayerController>().GetSprinting();
	   movementInput = edward.GetComponent<PlayerController>().GetMovementInput();
	   playerSpeed = edward.GetComponent<PlayerController>().GetPlayerSpeed();
	}
	
}

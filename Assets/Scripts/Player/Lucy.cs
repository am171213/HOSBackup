using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucy : MonoBehaviour
{
	private Animator anim;
    public bool lucyTransform = false;
	public bool lucyDisappear = false;
	
	void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (lucyTransform && !lucyDisappear)
		{
			anim.SetBool("transform", true);
			
			lucyTransform = false;
		}
		
		if (lucyDisappear)
		{
			anim.SetBool("transform", false);
			anim.SetBool("disappear", true);
			
			lucyDisappear = false;
		}
	}
}

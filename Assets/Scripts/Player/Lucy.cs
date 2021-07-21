using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucy : MonoBehaviour
{
	private Animator anim;
    private bool lucyTransform = false;
	private bool lucyDisappear = false;
	
	void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (lucyTransform)
		{
			
		}
	}
}

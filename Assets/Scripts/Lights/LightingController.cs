using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{
	private GameObject lighting;


    void Start()
    {
		lighting = GameObject.Find("GlobalLight");
    }
		
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class EmLightSwitch : MonoBehaviour
{
   public GameObject[] lights;
   
   public void TurnOnLights()
   {
	   for (int i=0; i < lights.Length; i++)
	   {
		   lights[i].GetComponent<Animator>().SetBool("TurnOn", true);
	   }
   }

	

}

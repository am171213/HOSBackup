using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalLightController : MonoBehaviour
{
	private static GlobalLightController masterInstance;
	
	public void Awake()
	{
		DontDestroyOnLoad (this);
		if (masterInstance == null) {
			masterInstance = this;
		} else {
			Destroy (gameObject);
		}		
	}
}

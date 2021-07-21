using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class SwitchController : MonoBehaviour
{
	public string customText;
	public Sprite tvOffSprite;
	private bool tvOff;
	public GameObject player;

	void Start()
	{
		player = GameObject.Find ("Edward");
	}

	void Update()
	{
			gameObject.GetComponent<SpriteRenderer> ().sprite = tvOffSprite;
			gameObject.GetComponent<Light2D> ().intensity = 0;
	}
}
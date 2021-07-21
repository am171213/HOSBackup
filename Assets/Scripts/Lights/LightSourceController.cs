using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{
	public AnimationClip[] anims;
	public string[] titles;
	public Vector3[] positions;
	public GameObject[] lightTypes;

	private static int pChoice = -1;
	private Animator anim;
	private GameObject lightObject;
	private GameMaster gm;

	void Start()
	{
		gm = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		anim =gameObject.GetComponent<Animator> ();
		pChoice = gm.GetDifficulty ();
		SetLightSource (pChoice);
	}
		
	public void SetLightSource (int choice)
	{
		if (choice != -1) {
			foreach (Transform child in transform) {
				GameObject.Destroy (child.gameObject);
			}
			anim.Play (titles[choice]);
			gameObject.transform.localPosition = positions [choice];
			lightObject = Instantiate (lightTypes [choice]);
			lightObject.transform.SetParent (gameObject.transform);
			lightObject.transform.localPosition = new Vector3 (0, 0, 0);
		}
	}

	public void FlipLight(bool isFlipped)
	{
		if (isFlipped) {
			gameObject.transform.localPosition = new Vector3 (-gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
			gameObject.transform.localRotation = Quaternion.Euler (0, 180, 0);
		}
		else {
			gameObject.transform.localPosition = positions [pChoice];
			gameObject.transform.localRotation = Quaternion.Euler (0, 0, 0);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static AudioClip playerWalkingSound;
	private static AudioSource audioControl;
    // Start is called before the first frame update
    void Start()
    {
		playerWalkingSound = Resources.Load<AudioClip> ("Walking2");

		audioControl = GetComponent<AudioSource> ();
    }


	public static void PlaySound (string clip){
		switch (clip) {
		case "walking": 
			audioControl.PlayOneShot (playerWalkingSound);
			break;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
	//Determining Which Array To Grab Sprite From
	public bool isBloodyTop, isBloodyMid, isBloodyBot;
	public bool isMoldyTop, isMoldyMid, isMoldyBot;

	//Bloody Sprite Arrays
	public Sprite[] bloodyTopSprites;		//4
	public Sprite[] bloodyMidSprites;		//3
	public Sprite[] bloodyBotSprites;		//2

	//Moldy Sprite Arrays
	public Sprite[] moldyTopSprites;		//6
	public Sprite[] moldyMidSprites;		//5
	public Sprite[] moldyBotSprites;		//2
	
	//Default
	public Sprite DefaultTop, DefaultMiddle, DefaultBottom;

	//Child Game Objects
	public GameObject topPanel, midPanel, botPanel;
	public GameObject wallSpriteObject;

	//Shortcuts
	private SpriteRenderer topSpRend, midSpRend, botSpRend;
	
	//public int numberToSpawn = 0;

    void Start()
    {
		//Sprite Renderer Call Shortcuts
		topSpRend = topPanel.GetComponent<SpriteRenderer> ();
		midSpRend = midPanel.GetComponent<SpriteRenderer> ();
		botSpRend = botPanel.GetComponent<SpriteRenderer> ();


		if (isBloodyTop) {
			SetTopSpritesBloody (bloodyTopSprites);
		} if (isBloodyMid) {
			SetMiddleSpritesBloody (bloodyMidSprites);
		} if (isBloodyBot) {
			SetBottomSpritesBloody (bloodyBotSprites);
		}

		if (isMoldyTop) {
			SetTopSpritesMoldy (moldyTopSprites);
		} if (isMoldyMid) {
			SetMiddleSpritesMoldy (moldyMidSprites);
		} if (isMoldyBot) {
			SetBottomSpritesMoldy (moldyBotSprites);
		}
		
		//CreateWalls(numberToSpawn);
	}
	/*
	private void CreateWalls(int number)
	{
		for (int i =0; i < number; i++)
		{
			Debug.Log("Made wall");
			Instantiate(wallSpriteObject, new Vector3(gameObject.transform.position.x+i * 2f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
		}
	}
		*/
	void SetTopSpritesBloody(Sprite[] bloodyTopSprites)
	{
		topSpRend.sprite = bloodyTopSprites[RandomNumber (0, bloodyTopSprites.Length)];
	}

	void SetMiddleSpritesBloody(Sprite[] bloodyMidSprites)
	{
		midSpRend.sprite = bloodyMidSprites[RandomNumber (0, bloodyMidSprites.Length)];
	}

	void SetBottomSpritesBloody(Sprite[] bloodyBotSprites)
	{
		botSpRend.sprite = bloodyBotSprites[RandomNumber (0, bloodyBotSprites.Length)];
	}

	void SetTopSpritesMoldy(Sprite[] moldyTopSprites)
	{
		topSpRend.sprite = moldyTopSprites[RandomNumber (0, moldyTopSprites.Length)];
	}

	void SetMiddleSpritesMoldy(Sprite[] moldyMidSprites)
	{
		midSpRend.sprite = moldyMidSprites[RandomNumber (0, moldyMidSprites.Length)];
	}

	void SetBottomSpritesMoldy(Sprite[] moldyBotSprites)
	{
		botSpRend.sprite = moldyBotSprites[RandomNumber (0, moldyBotSprites.Length)];
	}

	int RandomNumber (int min, int max)
	{
		int random = Random.Range (min, max);
		return (random);
	}
}

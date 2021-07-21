using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    // Start is called before the first frame update
	public bool isMoldy;
	public bool isBloody;
	
	public Sprite[] bloodyTiles;
	public Sprite[] moldyTiles;
	public Sprite defaultSprite;
	

    void Awake()
    {
		if(isBloody)
		{
			GetComponent<SpriteRenderer>().sprite = bloodyTiles[RandomNumber (0, bloodyTiles.Length)];
		}
		if(isMoldy)
		{
			GetComponent<SpriteRenderer>().sprite = moldyTiles[RandomNumber (0, moldyTiles.Length)];
		}
		else if(!isBloody && !isMoldy)
		{
			GetComponent<SpriteRenderer>().sprite = defaultSprite;
		}
    }
	
	int RandomNumber (int min, int max)
	{
		int random = Random.Range (min, max);
		return (random);
	}
}

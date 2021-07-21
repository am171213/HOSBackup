using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
	public string name;
	[TextArea(3,10)]
	public string[] textToSay;
	[TextArea(3,10)]
	public string[] altTextToSay;
	public string instructText;
	
	public void SwitchText(bool isAltText)
	{
		textToSay = altTextToSay;
	}
}

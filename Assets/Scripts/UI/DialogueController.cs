using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	private Queue<string> textToSay;
	public Text nameText;
	public Text dialogueText;
	public Animator anim;
	public GameObject txtBox;
	public GameObject player;
	public float typingSpeed;
	
    void Start()
    {
        textToSay = new Queue<string>();
    }
	
	public void SetTextToSay(string[] newText)
	{
		textToSay.Clear();
		for (int i = 0; i < newText.Length; i++)
		{
			textToSay.Enqueue(newText[i]);
		}

	}
	
	public void StartDialogue(Dialogue dialogue)
	{
		textToSay.Clear();
		anim.SetBool("IsOpen",true);
		nameText.text = dialogue.name;
		txtBox.SetActive(true);

		foreach (string line in dialogue.textToSay)
		{
			textToSay.Enqueue(line);
		}
		DisplayNextSentence();
	}
	
	public void DisplayNextSentence()
	{
		dialogueText.text = "";
		if (textToSay.Count == 0)
		{
			EndDialogue();
			return;
		}
		
		string line = textToSay.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(line));
	}
	
	IEnumerator TypeSentence (string line)
	{
		foreach(char letter in line.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(1-(typingSpeed*.01f));
		}
	}
	
	public void EndDialogue()
	{
		StopAllCoroutines();
		textToSay.Clear();
		txtBox.SetActive(false);
		nameText.text = "";
		dialogueText.text = "";
		anim.SetBool("IsOpen",false);
		player.GetComponent<PlayerController>().SetInteracting(false);
	}

}

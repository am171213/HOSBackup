using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item : MonoBehaviour
{
	public string itemName;
	public Sprite itemPic;
	public string itemCode;
	public bool itemRemoval;

	public Item(string name, Sprite pic, string code, bool removeAfterUse)
	{
		itemName = name;
		itemPic = pic;
		itemCode = code;
		itemRemoval = removeAfterUse;
	}
	
	public string GetItemName()
	{
		return itemName;
	}
	
	public string GetItemCode()
	{
		return itemCode;
	}
	
	public Sprite GetItemPic()
	{
		return itemPic;
	}
	
	public bool GetItemRemoval()
	{
		return itemRemoval;
	}
	
	public void SetItemName(string name)
	{
		itemName = name;
	}
	
	public void SetItemCode(string code)
	{
		itemCode = code;
	}
	
	public void SetItemPic(Sprite pic)
	{
		itemPic = pic;
	}	
	
	public void SetItemRemoval(bool removeItem)
	{
		itemRemoval = removeItem;
	}
}

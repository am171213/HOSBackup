using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	public Animator anim;
	public GameObject invPic;
	public int numItems = 0;
	[SerializeField]
	public List<Item> heldItems = new List<Item>();
	private GameObject inventory;
	private Transform tran;
	private const int maxItems = 10;
	private bool inventoryOpen = false;
	private int lastSlot = 0;

	
    private void Start()
    {
        invPic.SetActive(false);
		inventory = GameObject.Find("InventoryBox");
		tran = inventory.transform;
    }
	
	private void Update()
	{
		if(inventoryOpen)
		{
			OpenInventory();
		}
	}
	
	public void OpenInventory()
	{
		inventoryOpen = true;
		invPic.SetActive(true);
		ShowInventory();
	}
	
	public void CloseInventory()
	{
		inventoryOpen = false;
		invPic.SetActive(false);
		HideInventory();
	}
	
	public void AddToInventory(GameObject newItem)
	{	
		if (newItem != null && heldItems.Count <= maxItems)
		{	
			if (numItems == 0)
			{
				SetItem(newItem.GetComponent<Item>(),1);
			}
			else
			{
				SetItem(newItem.GetComponent<Item>(),OpenSpot());
			}
			numItems++;
			lastSlot = numItems;
		}
	}
	
	public bool FindItem(string itemCodeToFind)
	{
		for (int i = 0; i < heldItems.Count; i ++)
		{
			if (heldItems[i].GetComponent<Item>().GetItemCode() == itemCodeToFind)
			{
				return true;
			}
		}
		return false;
	}
	
	public GameObject GetItemSlot(int slotNum)
	{
		return tran.GetChild(slotNum).gameObject;
	}
	
	public Item GetItem(int itemNum)
	{
		if (itemNum <= lastSlot)
		{
			if (heldItems[itemNum-1] != null)
			{
				return heldItems[itemNum-1].GetComponent<Item>();
			}
			else
			{
				Debug.Log("Item doesn't exist at this slot.");
				return null;
			}
		}
		else
		{
			Debug.Log("Invalid item number.");
			return null;
		}
	}
	
	public void SetItem(Item newItem, int index)
	{
		GameObject itemSlot = GetItemSlot(index);
		heldItems.Add(itemSlot.AddComponent<Item>());
		
		itemSlot.GetComponent<Item>().SetItemName(newItem.GetItemName());
		itemSlot.GetComponent<Item>().SetItemCode(newItem.GetItemCode());
		itemSlot.GetComponent<Item>().SetItemPic(newItem.GetItemPic());
		itemSlot.GetComponent<Item>().SetItemRemoval(newItem.GetItemRemoval());
		itemSlot.GetComponent<Image>().enabled = true;
		itemSlot.GetComponent<Image>().sprite = newItem.GetComponent<Item>().GetItemPic();
		
		itemSlot.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = newItem.GetItemName();
		
	}
	
	public int OpenSpot()
	{
		int slotNum = numItems;
		
		for(int i = 1; i <= maxItems; i++)
		{
			if (GetItemSlot(i).GetComponent<Item>() == null)
			{
				//Debug.Log("Item at slot: " + i + " is null.");
				slotNum = i;
				break;
			}
		}
		//Debug.Log("The next open slot is: " + slotNum);
		return slotNum;
	}
	
	public void PrintInventory()
	{
		for (int i = 0; i < heldItems.Count; i++)
		{
			Debug.Log(heldItems[i].GetComponent<Item>().GetItemName());
		}
	}
	
	public void RemoveFromInventory(int slotNum)
	{
		string code = "";
		if (GetItemSlot(slotNum).GetComponent<Item>() != null)
		{
			code = GetItemSlot(slotNum).GetComponent<Item>().GetItemCode();
			if (heldItems.Count >= 0 && FindItem(code) && code != "")
			{		
				RemoveItem(slotNum);
				GetItemSlot(slotNum).GetComponent<Image>().enabled = false;
				
				numItems--;
			}
		}	
		//Debug.Log("Removed item");
		//PrintInventory();
	}
	
	public bool IsItemValid(int slotNum)
	{
		if (GetItemSlot(slotNum).GetComponent<Item>() != null)
		{
			//Debug.Log("An item is present at slot number: " + slotNum);
			return true;
		}
		else
		{
			//Debug.Log("No item at slot number: " + slotNum);
			return false;
		}
	}
	
	public void RemoveItem(int slotNum)
	{
		GetItemSlot(slotNum).GetComponent<Image>().sprite = null;
		GetItemSlot(slotNum).GetComponent<Item>().SetItemName("");
		GetItemSlot(slotNum).GetComponent<Item>().SetItemCode("");
		GetItemSlot(slotNum).GetComponent<Item>().SetItemPic(null);
		
		GetItemSlot(slotNum).GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "";
	}
	
	public void ShowInventory()
	{
		for (int i = 0; i <= lastSlot; i++)
		{
			inventory.transform.GetChild(i).gameObject.SetActive(true);
		}
	}
	
	
	public void HideInventory()
	{
		for (int i = 0; i <= lastSlot; i++)
		{
			inventory.transform.GetChild(i).gameObject.SetActive(false);
		}
	}
	
	public bool CheckItemMatching(int itemNum, GameObject interactObj)
	{
		if (interactObj.GetComponent<Interactable>() != null && GetItemSlot(itemNum).GetComponent<Item>() != null)
		{
			string objectCode = interactObj.GetComponent<Interactable>().GetObjectCode();
			//Debug.Log("The item number is: " + itemNum);
			
			string itemCode = GetItem(itemNum).GetItemCode();
			
			if (objectCode == itemCode)
			{
				//Debug.Log("Object Code: " + objectCode + " and Item Code: " + itemCode + " MATCH");
				return true;
			}
			else
			{
				//Debug.Log("Object Code: " + objectCode + " and Item Code: " + itemCode + " DO NOT MATCH");
				return false;
			}
		}
		else
		{
			//Debug.Log("Either the interactingObject or the item is null");
			return false;
		}
	}
	
}

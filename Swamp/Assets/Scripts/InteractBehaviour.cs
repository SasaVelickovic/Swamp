using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
	[SerializeField]
	private MoveBehaviour playerMoveBehaviour;
	
	[SerializeField]
	private Animator playerAnimator;
	
	[SerializeField]
	public Inventory inventory;
	
	private Item currentItem;
	
	public void DoPickup(Item item)
	{
		if(inventory.IsFull())
		{
			Debug.Log("Inventory full, can't pick up : " + item.name);
			return;
		}
		
		currentItem = item;
		
		playerAnimator.SetTrigger("Pickup"); 
		playerMoveBehaviour.canMove = false;
	}
	
	public void DoHarvest(Harvestable harvestable)
	{
		playerAnimator.SetTrigger("Harvest");
		playerMoveBehaviour.canMove = false;
	}
	
	public void AddItemToInventory()
	{
		inventory.AddItem(currentItem.itemData);
		Destroy(currentItem.gameObject);
		
		currentItem = null;
	}
	
	public void ReEnablePlayerMovement()
	{
		playerMoveBehaviour.canMove = true;
	}
}

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	
	[Header("OTHER SCRIPTS REFERENCES")]
	
	[SerializeField]
	private Equipment equipment;
	
	[SerializeField]
	private ItemActionSystem itemActionSystem;
	
	[Header("INVENTORY SYSTEM VARIABLES")]
	
	[SerializeField]
    public List<ItemData> content = new List<ItemData>();
	
	[SerializeField]
	private GameObject inventoryPanel;
	
	[SerializeField]
	private Transform inventorySlotsParent;
	
	public Sprite emptySlotVisual;
	
	public static Inventory instance;
	
	const int InventorySize = 54;
	private bool isOpen = false;
	
	private void Awake()
	{
	 	instance = this;
	}
	
	private void Start()
	{
		CloseInventory();
		RefreshContent();
	}
	
	public void AddItem(ItemData item)
	{
		content.Add(item); 
		RefreshContent();
	}
	
	public void RemoveItem(ItemData item)
	{
		content.Remove(item); 
		RefreshContent();
	}
	
	private void OpenInventory()
	{
		inventoryPanel.SetActive(true);
		isOpen = true;
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			if(isOpen)
			{
				CloseInventory();
			}
			else
			{
				OpenInventory();
			}
		}
	}
	
	public void CloseInventory()
		{
			inventoryPanel.SetActive(false);
			itemActionSystem.actionPanel.SetActive(false);
			TooltipSystem.instance.Hide();
			isOpen = false;
		}
	
	public void RefreshContent()
	{
			//On vide tout les slots /visuels
			for (int i = 0; i < inventorySlotsParent.childCount; i++)
			{
				Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();
				
				currentSlot.item = null;
				currentSlot.itemVisual.sprite = emptySlotVisual;
			} 
			
			//On peuple le visuel des slots selon le contenu reél de l'inventaire
			for (int i = 0; i < content.Count; i++)
			{
				Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();
				
				currentSlot.item = content[i];
				currentSlot.itemVisual.sprite = content[i].visual;
			}  
			
			equipment.UpdateEquipmentDesequipButton();
			
	}
	
	public bool IsFull()
	{
		return InventorySize == content.Count;
	}
	
}

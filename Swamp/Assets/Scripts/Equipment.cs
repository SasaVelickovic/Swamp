using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Equipment : MonoBehaviour
{
	[Header("OTHER SCRIPTS REFERENCES")]
	
	[SerializeField]
	private ItemActionSystem itemActionSystem;
	
    [Header("EQUIPMENT SYSTEM VARIABLES")] 
	
	[SerializeField]
	private EquipmentLibrary equipmentLibrary;
	
	[SerializeField]
	private Image headSlotImage;
	
	[SerializeField]
	private Image chestSlotImage;
	
	[SerializeField]
	private Image handSlotImage;
	
	[SerializeField]
	private Image legsSlotImage;
	
	[SerializeField]
	private Image feetSlotImage;
	
	//Garde une treace des équipments actuels
	private ItemData equipHeadItem;
	private ItemData equipChestItem;
	private ItemData equipHandItem;
	private ItemData equipLegsItem;
	private ItemData equipFeetItem;
	
	[SerializeField]
	private Button headSlotDesequipButton;
	
	[SerializeField]
	private Button chestSlotDesequipButton;
	
	[SerializeField]
	private Button handSlotDesequipButton;
	
	[SerializeField]
	private Button legsSlotDesequipButton;
	
	[SerializeField]
	private Button feetSlotDesequipButton;
	
	private void DisablePreviousEquipedEquipment(ItemData itemToDisable)
	{
		if(itemToDisable == null)
		{
			return;
		}
	
		EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemToDisable).First();
	
	    if(equipmentLibraryItem != null)
		{
			for (int i = 0; i < equipmentLibraryItem.elementsToDisable.Length; i++)
			{
				equipmentLibraryItem.elementsToDisable[i].SetActive(true);
			}
		
			equipmentLibraryItem.itemPrefab.SetActive(false);
		}
		
		Inventory.instance.AddItem(itemToDisable);
	}
	
	public void DesequipEquipment(EquipmentType equipmentType)
	{
		
		if(Inventory.instance.IsFull())
		{
			Debug.Log("L'inventaire est plein ");
			return;
		}
		
		ItemData currentItem = null;
		
		switch(equipmentType)
		{
			case EquipmentType.Head:
				currentItem = equipHeadItem;
				equipHeadItem = null;
				headSlotImage.sprite = Inventory.instance.emptySlotVisual;
				break;
				
			case EquipmentType.Chest:
				currentItem = equipChestItem;
				equipChestItem = null;
				chestSlotImage.sprite = Inventory.instance.emptySlotVisual;
				break;
				
			case EquipmentType.Hand:
				currentItem = equipHandItem;
				equipHandItem = null;
				handSlotImage.sprite = Inventory.instance.emptySlotVisual;
				break;
				
			case EquipmentType.Legs:
				currentItem = equipLegsItem;
				equipLegsItem = null;
				legsSlotImage.sprite = Inventory.instance.emptySlotVisual;
				break;

			case EquipmentType.Feet:
				currentItem = equipFeetItem;
				equipFeetItem = null;
				feetSlotImage.sprite = Inventory.instance.emptySlotVisual;
				break;
				
		}
				
	    EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == currentItem).First();
	
	    if(equipmentLibraryItem != null)
		{
			for (int i = 0; i < equipmentLibraryItem.elementsToDisable.Length; i++)
			{
				equipmentLibraryItem.elementsToDisable[i].SetActive(true);
			}
		
			equipmentLibraryItem.itemPrefab.SetActive(false);
		}
		
		Inventory.instance.AddItem(currentItem);
		Inventory.instance.RefreshContent();
		
	}
	
		public void UpdateEquipmentDesequipButton()
	{
		headSlotDesequipButton.onClick.RemoveAllListeners();
		headSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Head); });
		headSlotDesequipButton.gameObject.SetActive(equipHeadItem);
		
		chestSlotDesequipButton.onClick.RemoveAllListeners();
	chestSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Chest); });
		chestSlotDesequipButton.gameObject.SetActive(equipChestItem);
		
		handSlotDesequipButton.onClick.RemoveAllListeners();
		handSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Hand); });
		handSlotDesequipButton.gameObject.SetActive(equipHandItem);
		
		legsSlotDesequipButton.onClick.RemoveAllListeners();
	legsSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Legs); });
		legsSlotDesequipButton.gameObject.SetActive(equipLegsItem);
		
		feetSlotDesequipButton.onClick.RemoveAllListeners();
	feetSlotDesequipButton.onClick.AddListener(delegate {DesequipEquipment(EquipmentType.Feet); });
		feetSlotDesequipButton.gameObject.SetActive(equipFeetItem);
		
	}
	
	public void EquipAction()
	{
		print("Equip item : " + itemActionSystem.itemCurrentlySelected.name);
		
	    EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemActionSystem.itemCurrentlySelected).First();
		
		switch(itemActionSystem.itemCurrentlySelected.equipmentType)
		{
			case EquipmentType.Head:
				DisablePreviousEquipedEquipment(equipHeadItem);
				headSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual;
				equipHeadItem = itemActionSystem.itemCurrentlySelected;
				break;
				
			case EquipmentType.Chest:
				DisablePreviousEquipedEquipment(equipChestItem);
				chestSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual;
				equipChestItem = itemActionSystem.itemCurrentlySelected;
				break;
				
			case EquipmentType.Hand:
				DisablePreviousEquipedEquipment(equipHandItem);
				handSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual;
				equipHandItem = itemActionSystem.itemCurrentlySelected;
				break;
								
			case EquipmentType.Legs:
				DisablePreviousEquipedEquipment(equipLegsItem);
				legsSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual;
				equipLegsItem = itemActionSystem.itemCurrentlySelected;
				break;
								
			case EquipmentType.Feet:
				DisablePreviousEquipedEquipment(equipFeetItem);
				feetSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual;
				equipFeetItem = itemActionSystem.itemCurrentlySelected;
				break;
								
		}		
	
		if(equipmentLibraryItem != null)
		{
		for (int i = 0; i < equipmentLibraryItem.elementsToDisable.Length; i++)
			{
				equipmentLibraryItem.elementsToDisable[i].SetActive(false);
			}
		
			equipmentLibraryItem.itemPrefab.SetActive(true);
		
			Inventory.instance.RemoveItem(itemActionSystem.itemCurrentlySelected);
		}
		else
		{
			Debug.LogError("Equipment :" + itemActionSystem.itemCurrentlySelected.name + "nonn existant");
		}
		
			itemActionSystem.CloseActionPanel();
	}
}
	


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private List<RectTransform> inventoryItemsUI;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        inventoryItemsUI = new List<RectTransform>();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        inventory.OnItemSelected += Inventory_OnItemSelected;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        float itemSlotCellSize = 95f;
        inventoryItemsUI.Clear();
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);

            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            uiText.SetText(item.amount.ToString());

            inventoryItemsUI.Add(itemSlotRectTransform);

            if (item.position == inventory.GetSelectedItem())
            {
                itemSlotRectTransform.Find("border").gameObject.SetActive(true);
            }

            x++;

            item.position = x;
        }
    }

    private void Inventory_OnItemSelected(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
        int selectedItem = inventory.GetSelectedItem();
        if (selectedItem > 0)
        {   
            RectTransform itemUI = inventoryItemsUI[selectedItem-1];
            itemUI.Find("border").gameObject.SetActive(true);
        }
    }
}

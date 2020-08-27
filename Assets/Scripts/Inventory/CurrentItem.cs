using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    [HideInInspector]
    public int index;

    private GameObject inventoryObject;
    private Inventory inventory;

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();        
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedObject = Drag.dragedObj;
        if (dragedObject == null)
        {
            return;
        }
        CurrentItem currentDragedItem = dragedObject.GetComponent<CurrentItem>();
        if (currentDragedItem != null)
        {
            ItemInventory currentItem = inventory.Items[GetComponent<CurrentItem>().index];
            inventory.Items[GetComponent<CurrentItem>().index] = inventory.Items[currentDragedItem.index];
            inventory.Items[currentDragedItem.index] = currentItem;
            inventory.DisplayItems();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {        
        //if (eventData.button == PointerEventData.InputButton.Left)
        //{
        //    if(inventory.Items[index].CustomEvenat != null)
        //    {
        //        inventory.Items[index].CustomEvent.Invoke();
        //    }
        //}

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventory.Items[index].ItemId > 0 && inventory.PopUpMenu.activeSelf == false)
            {
                if (inventory.PopUpMenu.activeSelf == false)
                {
                    Transform cell = inventory.cellContainer.transform.GetChild(index);
                    inventory.PopUpMenu.transform.position = cell.position;
                    inventory.PopUpMenu.transform.position += new Vector3(inventory.cellContainer.GetComponent<GridLayoutGroup>().cellSize.x, 0);
                    
                    inventory.PopUpMenu.SetActive(true);
                }
                else
                {
                    inventory.PopUpMenu.SetActive(false);
                }
            }
            else
            {
                inventory.PopUpMenu.SetActive(false);
            }
            
           
            
            //if (inventory.Items[index].IsDroped == true)
            //{
            //    Drop();
            //    Remove();
            //}
        }
    }
    
    public void Remove()
    {
        if (inventory.Items[index].ItemCount > 1)
        {
            inventory.Items[index].ItemCount--;
        }
        else
        {
            inventory.Items[index] = new ItemInventory();
        }
        inventory.DisplayItems();
    }

    public void Drop()
    {
        if (inventory.Items[index].ItemId != 0)
        {
            for (int i = 0; i < inventory.DataBase.transform.childCount; i++)
            {
                ItemInventory item = inventory.DataBase.transform.GetChild(i).GetComponent<ItemInventory>();
                if(item != null)
                {
                    if (inventory.Items[index].ItemId == item.ItemId)
                    {
                        GameObject dropedObject = Instantiate(item.gameObject);
                        dropedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
                    }
                }
            }          
        }
    }
}

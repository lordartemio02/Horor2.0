using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject dragedObj;

    Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragedObj = gameObject;
        inventory.DragPrefab.SetActive(true);
        inventory.DragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;

        if(dragedObj.GetComponent<CurrentItem>() != null)
        {
            int index = dragedObj.GetComponent<CurrentItem>().index;
            inventory.DragPrefab.GetComponent<Image>().sprite = inventory.Items[index].Icon;
            if (inventory.Items[index].ItemCount > 1)
            {
                inventory.DragPrefab.transform.GetChild(0).GetComponent<Text>().text = inventory.Items[index].ItemCount.ToString();
            }
            else
            {
                inventory.DragPrefab.transform.GetChild(0).GetComponent<Text>().text = null;
            }
            if (inventory.DragPrefab.GetComponent<Image>().sprite == null)
            {
                dragedObj = null;
                inventory.DragPrefab.SetActive(false);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.DragPrefab.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (inventory.DragPrefab.GetComponent<Image>().sprite != null)
            {
                if (inventory.Items[dragedObj.GetComponent<CurrentItem>().index].IsDroped == true)
                {
                    dragedObj.GetComponent<CurrentItem>().Drop();
                    dragedObj.GetComponent<CurrentItem>().Remove();
                }                
            }          
        }

        dragedObj = null;
        inventory.DragPrefab.SetActive(false);
        inventory.DragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

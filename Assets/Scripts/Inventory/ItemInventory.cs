using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInventory : MonoBehaviour
{
    public string ItemName;
    public int ItemId;
    [HideInInspector]
    public int ItemCount;
    public bool IsStackable;
    public bool IsDroped;
    [Multiline(5)]
    public string ItemDescription;
    public Sprite Icon;
    //public UnityEvent CustomEvent;
}

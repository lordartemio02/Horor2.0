using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteraction
{

    public void Use()
    {
        Debug.Log("Take Item");
    }
}

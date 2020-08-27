using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour, IInteraction
{

    public void Use()
    {
        Debug.Log("Take Item");
    }
}

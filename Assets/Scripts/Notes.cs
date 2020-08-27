using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour, IInteraction
{
    public void Use()
    {
        Debug.Log("Take Notes");
    }
}

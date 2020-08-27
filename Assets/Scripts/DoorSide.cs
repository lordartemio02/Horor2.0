using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSide : MonoBehaviour, IInteraction
{

    public bool DoorSideBool;
    public Door Door;

    public void Use()
    {
        Door.OpenDoor(DoorSideBool);
    }
}

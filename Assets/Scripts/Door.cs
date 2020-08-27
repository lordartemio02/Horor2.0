using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    private Player player;
    public bool OpenedDoor;
    public bool DoorSideBoolHEllo;
    [SerializeField] private Animator animator;
    public void OpenDoor(bool DoorSideBool)
    {
        if (OpenedDoor == false)
        {
            if (DoorSideBool == true)
            {
                Debug.Log("hello123");
                animator.SetInteger("id", 1);
                OpenedDoor = true;
            }
            if (DoorSideBool == false)
            {

                animator.SetInteger("id", 2);
                OpenedDoor = true;
            }
        }
        else
        {
            if (DoorSideBool == true)
            {
                Debug.Log("hello123333");
                animator.SetInteger("id", 0);
                OpenedDoor = false;
            }
            if (DoorSideBool == false)
            {
                Debug.Log("hello123333");
                animator.SetInteger("id", 0);
                OpenedDoor = false;
            }
        }

    }

    //private void OnDisable()
    //{
    //    animDoor.Stop();
    //}
}

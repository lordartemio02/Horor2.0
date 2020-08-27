using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]private GameObject objPlayer;

    public Transform posPlayer;
    private void FixedUpdate()
    {
        posPlayer = objPlayer.transform;
        Debug.Log(posPlayer);
    }
}

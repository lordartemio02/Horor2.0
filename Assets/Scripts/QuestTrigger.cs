using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField]
    private String textQuest;

    public GameObject[] ListNextQuests;

    public GameObject[] ListAlternativeQuests;
     
    void Start()
    {
          
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
        }         
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

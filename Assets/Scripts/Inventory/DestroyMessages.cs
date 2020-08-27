using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMessages : MonoBehaviour
{
    [SerializeField]
    private float time = 1;
    void Update()
    {
        Destroy(gameObject, time);
    }
}

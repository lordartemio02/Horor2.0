using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    [SerializeField]private List<Transform> pointRun;
    public int IndexPoint = -1;
    public Transform GetPointNext()
    {
        if (IndexPoint < pointRun.Count-1)
        {
            IndexPoint++;
        }
        else 
        {
            IndexPoint = 0;
        }
        return pointRun[IndexPoint];
    }

}

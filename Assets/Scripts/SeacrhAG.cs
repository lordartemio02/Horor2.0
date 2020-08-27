using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SeacrhAG : MonoBehaviour
{
    [SerializeField]private GameObject rayCastAgent;
    public LayerMask LayerMask;
    public int rays = 6;
    public Vector3 OffSet;
    private Transform target;
    public string TargetTag = "Player";
    public float Angle = 20;
    private AICharacterControl characterControlAI;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(TargetTag).transform;
        
    }

    private void Awake()
    {
        characterControlAI = GetComponent<AICharacterControl>();
    }
    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 pos = transform.position + OffSet;
        if (Physics.Raycast(pos, dir, out hit, Mathf.Infinity))
        {
            if (hit.transform == target)
            {
                result = true;
                Debug.DrawLine(pos, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(pos, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(pos, dir * Mathf.Infinity, Color.red);
        }
        return result;
    }
    public bool FoundPlayer()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;
        for (int i = 0; i < rays; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += Angle * Mathf.Deg2Rad / rays;

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
            if (GetRaycast(dir)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(dir)) b = true;
            }
        }

        if (a || b) result = true;
        return result;
    }
}

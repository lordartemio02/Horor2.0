using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public int rays = 1;
    public Vector3 OffSet;
    private Transform target;
    public string TargetTag = "Use";
    public float Angle = 20;
    public Camera Camera;
    public int distance = 3;
    public bool PlayerContoll;
    private FirstPersonController firstPersonController;
    public bool PlayerRun;
    public bool PlayerStand;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(TargetTag).transform;
    }
    private void Awake()
    {
        //firstPersonController = GetComponent<FirstPersonController>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
    }
    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 pos = transform.position + OffSet;
        if (Physics.Raycast(pos, dir, out hit, distance))
        {
            if (hit.transform.tag == target.tag)
            {
                UIText.UITextName.TakeNameTrue(hit.transform.gameObject.name);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<IInteraction>().Use();
                    Debug.DrawLine(pos, hit.point, Color.green);
                    Debug.Log("Open door2");
                }
            }
            else
            {
                UIText.UITextName.TakeNameFalse();
                Debug.DrawLine(pos, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(pos, dir * distance, Color.red);
        }
        return result;
    }

    bool RayToScan()
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
    void Update()
    {
        PlayerRun = firstPersonController.PlayerRuning;
        PlayerStand = firstPersonController.PlayerStands;
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(transform.position, target.position) < distance)
            {
                if (RayToScan())
                {

                }  
            }
        }
    }
}

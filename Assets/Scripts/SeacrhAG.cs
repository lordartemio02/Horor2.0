using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SeacrhAG : MonoBehaviour
{
    public LayerMask LayerMask;
    public int rays = 6;
    public Vector3 OffSet;
    public Player Player;
    public const string  TargetTagPlayer = "Player";
    public float Angle = 20;
    public Door Door;
    public float BallsSoundStepsPlayer = 0;

    private Vector3 heading;
    private float distanceToPlayerHeard = 40;
    private Transform targetPlayer;
    private AICharacterControl characterControlAI;
    private int distance = 5;


    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag(TargetTagPlayer).transform;
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
            if (hit.transform == targetPlayer)
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
    public bool HeardPlayer()
    {
        bool result = false;
        heading = gameObject.transform.position - targetPlayer.position;
        //сюда запишется инфо о пересечении луча, если оно будет
        RaycastHit hit;
        //сам луч, начинается от позиции этого объекта и направлен в сторону цели
        Ray ray = new Ray(transform.position, targetPlayer.position - transform.position);
        //пускаем луч
        Physics.Raycast(ray, out hit);
            //если луч с чем-то пересёкся, то..
        if (hit.collider != null && heading.sqrMagnitude < distanceToPlayerHeard && Player.PlayerStand == false)
        {
        //если луч не попал в цель
            if (hit.collider.gameObject != targetPlayer.gameObject)
            {
                if (Player.PlayerRun == false && heading.sqrMagnitude < distanceToPlayerHeard && heading.sqrMagnitude > distanceToPlayerHeard/2)
                {
                    BallsSoundStepsPlayer += 2;
                }
                if (Player.PlayerRun == true && heading.sqrMagnitude < distanceToPlayerHeard / 2)
                {
                    BallsSoundStepsPlayer += 1;
                }
                if (Player.PlayerRun == true && heading.sqrMagnitude < distanceToPlayerHeard && heading.sqrMagnitude > distanceToPlayerHeard / 2) 
                {
                    if (BallsSoundStepsPlayer != 0)
                    {
                        Debug.Log("first");
                        BallsSoundStepsPlayer -= 2;
                    }
                }
                if (Player.PlayerRun == false && heading.sqrMagnitude < distanceToPlayerHeard / 2)
                {
                    BallsSoundStepsPlayer += 1;
                }
                Debug.Log("Путь к врагу преграждает объект: " + hit.collider.name);
            }
            //если луч попал в цель
            else
            {
                if (Player.PlayerRun == false)
                {
                    BallsSoundStepsPlayer += 4;
                }
                if (Player.PlayerRun == true)
                {
                    BallsSoundStepsPlayer += 2;
                }
                Debug.Log("Попадаю во врага!!!");
            }
                //просто для наглядности рисуем луч в окне Scene
                Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        if ((heading.sqrMagnitude > distanceToPlayerHeard || Player.PlayerStand == true) && BallsSoundStepsPlayer > 0)
        {
            Debug.Log("second");
            BallsSoundStepsPlayer -= 4;
        }
        if (BallsSoundStepsPlayer == 100)
        {
            Debug.Log(BallsSoundStepsPlayer);
            result = true;
        }
        return result;
    }
}

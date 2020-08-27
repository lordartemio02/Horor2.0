using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Brain : MonoBehaviour
{
    private PatrolAI patrolAI;
    private AICharacterControl characterControlAI;
    [SerializeField]private SeacrhAG seacrhAG;
    private Vector3 fwd;
    [SerializeField]private Transform player;
    [SerializeField]private Transform playerPoint;
    private FlagState state;


    private Vector3 distance;
    private Transform targetPoint;

    void Start()
    {
        targetPoint = patrolAI.GetPointNext();
        characterControlAI.SetTarget(targetPoint);
        fwd = transform.TransformDirection(Vector3.forward);
    }
    private void Awake()
    {
        patrolAI = GetComponent<PatrolAI>();
        characterControlAI = GetComponent<AICharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {

        distance = transform.position - targetPoint.position;
        if (distance.sqrMagnitude < 1 && state != FlagState.FoundPlayer)
        {

            targetPoint = patrolAI.GetPointNext();
            characterControlAI.SetTarget(targetPoint);
        }
        else
        {
            if (distance.sqrMagnitude <1)
            {
                state = FlagState.Patrol;
                targetPoint = patrolAI.GetPointNext();
                characterControlAI.SetTarget(targetPoint);
            }
        }
    }

    private void FixedUpdate()
    {
        if (seacrhAG.FoundPlayer() == true)
        {
            state = FlagState.FoundPlayer;
            playerPoint.position = player.position;
            Debug.Log("YA TEBYA YBIY");
            targetPoint = playerPoint;
            characterControlAI.SetTarget(playerPoint);
        }
    }
    enum FlagState
    {
        Patrol,
        FoundPlayer
    }
}

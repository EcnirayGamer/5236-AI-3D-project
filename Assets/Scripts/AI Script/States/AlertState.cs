﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : AbstractState
{
    [SerializeField] private GameObject stimulant;
    [SerializeField] private float chaseRadius;
    private GameObject player;
    private bool alert;
    private Vector3 playerLastPos;
    private float playerDistance;

    public void OnEnable()
    {
        base.OnEnable();
        player = GameObject.FindGameObjectWithTag("player");
        StateType = FSMStateType.ALERT;
        //alert = stimulant;
    }

    public override bool EnterState()
    {
        EnterdState = false;
        if (base.EnterState())
        {
            if (alert == true)
            {
                playerLastPos = player.transform.position;
                playerLastPostion(playerLastPos);
                EnterdState = true;
            }
        }
        return EnterdState;
    }

    public override void UpdateState()
    {
        if (EnterdState)
        {
            playerDistance = Vector3.Distance(_navMeshAgent.transform.position, playerLastPos);
            if (playerDistance <= chaseRadius && player.layer == 3)
            {
                _fsm.EnterState(FSMStateType.CHASING);
            }
            else if (Vector3.Distance(_navMeshAgent.transform.position, playerLastPos) <= 1f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }
        }
    }

    private void playerLastPostion(Vector3 playePostion)
    {
        if (_navMeshAgent != null && playePostion != null)
        {
            _navMeshAgent.SetDestination(playePostion);
        }
    }
}
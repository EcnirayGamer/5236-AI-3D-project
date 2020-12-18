using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patrol State", menuName = "AI-FSM/States/Patrol", order = 1)]
public class PatrolState : AbstractState
{
    [SerializeField] private GameObject player;
    [SerializeField] private float chaseRadius;
    private Waypoint[] _patrolPoints;
    private int _patrolPointIndex;

    public void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PATROL;
        _patrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        EnterdState = false;
        if (base.EnterState())
        {
            //Grab and store the patrol points.
            _patrolPoints = _enemyAI.PatrolPoints;
            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {
                Debug.LogError("PatrolState: Failed to grab patrol points from AI");
            }
            else
            {
                if (_patrolPointIndex < 0)
                {
                    _patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                }
                else
                {
                    _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                }
                SetDestination(_patrolPoints[_patrolPointIndex]);
                EnterdState = true;
            }
        }
        return EnterdState;
    }

    public override void UpdateState()
    {
        if (EnterdState)
        {
            if (Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) <= 1f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) >= chaseRadius)
            {
                _fsm.EnterState(FSMStateType.CHASING);
            }
        }
    }

    private void SetDestination(Waypoint waypoint)
    {
        if (_navMeshAgent != null && waypoint != null)
        {
            _navMeshAgent.SetDestination(waypoint.transform.position);
        }
    }
}
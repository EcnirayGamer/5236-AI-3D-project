using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.AI_Script;

public enum ExcutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED
}

public enum FSMStateType
{
    IDLE,
    PATROL,
    CHASING,
    ALERT
}

public abstract class AbstractState : ScriptableObject
{
    protected NavMeshAgent _navMeshAgent;
    protected EnemyAI _enemyAI;
    protected FiniteStateMachine _fsm;
    public ExcutionState ExcutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }

    public bool EnterdState { get; protected set; }

    public virtual void OnEnable()
    {
        ExcutionState = ExcutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successEnemyAI = true;
        ExcutionState = ExcutionState.ACTIVE;

        //Does the nav mesh agent exist
        successNavMesh = (_navMeshAgent != null);
        //Does the excuting agent exist
        successEnemyAI = (_enemyAI != null);
        return successEnemyAI && successNavMesh;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExcutionState = ExcutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExcutingEnemyAI(EnemyAI enemyAI)
    {
        if (enemyAI != null)
        {
            _enemyAI = enemyAI;
        }
    }

    public virtual void SetExcutingFSM(FiniteStateMachine fsm)
    {
        if (fsm != null)
        {
            _fsm = fsm;
        }
    }
}
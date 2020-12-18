using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.AI_Script;

public class FiniteStateMachine : MonoBehaviour
{
    private AbstractState _currentState;

    [SerializeField]
    private List<AbstractState> _validStates;

    private Dictionary<FSMStateType, AbstractState> _fsmStates;

    private void Awake()
    {
        _currentState = null;
        _fsmStates = new Dictionary<FSMStateType, AbstractState>();
        NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
        EnemyAI _enemyAI = this.GetComponent<EnemyAI>();

        foreach (AbstractState state in _validStates)
        {
            state.SetExcutingFSM(this);
            state.SetExcutingEnemyAI(_enemyAI);
            state.SetNavMeshAgent(navMeshAgent);
            _fsmStates.Add(state.StateType, state);
        }
    }

    private void Start()
    {
        EnterState(FSMStateType.IDLE);
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }

    #region State Mangement

    public void EnterState(AbstractState nextState)
    {
        if (nextState == null)
        {
            return;
        }
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = nextState;
        _currentState.EnterState();
    }

    public void EnterState(FSMStateType stateType)
    {
        if (_fsmStates.ContainsKey(stateType))
        {
            AbstractState nextState = _fsmStates[stateType];

            EnterState(nextState);
        }
    }

    #endregion State Mangement
}
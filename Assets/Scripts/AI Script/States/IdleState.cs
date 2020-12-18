using UnityEngine;
using System.Collections;
using UnityEditorInternal;

[CreateAssetMenu(fileName = "IdleState", menuName = "AI-FSM/States/Idle", order = 1)]
public class IdleState : AbstractState
{
    [SerializeField] private float _idelDuration = 3f;
    [SerializeField] private float chaseRadius;
    private GameObject player;
    private float _totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        player = GameObject.FindGameObjectWithTag("player");
        StateType = FSMStateType.IDLE;
        Debug.Log(player);
    }

    public override bool EnterState()
    {
        EnterdState = base.EnterState();
        if (EnterdState)
        {
            Debug.Log("Entered Idle State");
            _totalDuration = 0f;
        }
        return EnterdState;
    }

    public override void UpdateState()
    {
        if (EnterdState)
        {
            _totalDuration += Time.deltaTime;
            Debug.Log("Updateing Idle State: " + _totalDuration + " Secounds");

            if (_totalDuration >= _idelDuration)
            {
                _fsm.EnterState(FSMStateType.PATROL);
            }
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) >= chaseRadius)
            {
                _fsm.EnterState(FSMStateType.CHASING);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting Idle State");
        return true;
    }
}
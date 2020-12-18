using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI_Script
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private Waypoint[] _patrolPoints;

        private NavMeshAgent _navMeshAgent;

        private FiniteStateMachine fsm;

        private void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            fsm = this.GetComponent<FiniteStateMachine>();
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public Waypoint[] PatrolPoints
        {
            get
            {
                return _patrolPoints;
            }
        }
    }
}
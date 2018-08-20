using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Finite State Machine/Actions/Pursue")]
public class PursueAction : Action {

	public override void Execute(IStateMachine stateMachine)
	{
        var agent = stateMachine.GetAgent();
        var agentStats = agent.GetStats();
        var navMeshAgent = agent.GetNavMeshAgent();

        navMeshAgent.stoppingDistance = agentStats.PursueStoppingDistance;
        navMeshAgent.destination = agent.Target.position;
        navMeshAgent.isStopped = false;
        agent.RotateTowardsTarget();
	}
}

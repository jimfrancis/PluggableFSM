using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Finite State Machine/Actions/Patrol")]
public class PatrolAction : Action
{
	public override void Execute(IStateMachine stateMachine)
	{
		Patrol(stateMachine);
	}

	private void Patrol(IStateMachine stateMachine)
	{
        var navMeshAgent = stateMachine.GetAgent().GetNavMeshAgent();
        var agentStats = stateMachine.GetAgent().GetStats();
        var wayPoints = stateMachine.GetAgent().GetWayPoints();

        navMeshAgent.stoppingDistance = agentStats.PatrolStoppingDistance;
        navMeshAgent.destination = wayPoints[controller.NextWayPoint].position;
        navMeshAgent.isStopped = false;

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending) 
		{
			controller.NextWayPoint = (controller.NextWayPoint + 1) % controller.WayPointList.Count;
		}
	}
}

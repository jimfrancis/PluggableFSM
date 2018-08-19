using Assets.Scripts.FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Finite State Machine/Actions/Patrol")]
public class PatrolAction : Action
{
    public List<Transform> WayPointList;

    public override void Execute(IStateMachine stateMachine)
	{
        if (WayPointList == null) {
            throw new Exception($"No Waypoints found in Patrol Action for {stateMachine.GetAgent().gameObject.name}");
        }

		Patrol(stateMachine);
	}

	private void Patrol(IStateMachine stateMachine)
	{
        var agent = stateMachine.GetAgent();
        var navMeshAgent = agent.GetNavMeshAgent();
        var agentStats = agent.GetStats();

        navMeshAgent.stoppingDistance = agentStats.PatrolStoppingDistance;
        navMeshAgent.destination = WayPointList[agent.NextWaypoint].position;
        navMeshAgent.isStopped = false;
        agent.RotateTowardsTarget();

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending) 
		{
            agent.NextWaypoint = (agent.NextWaypoint + 1) % WayPointList.Count;
		}
	}
}

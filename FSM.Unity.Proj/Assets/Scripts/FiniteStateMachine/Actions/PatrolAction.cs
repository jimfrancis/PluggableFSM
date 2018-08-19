using Assets.Scripts.FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Finite State Machine/Actions/Patrol")]
public class PatrolAction : Action
{
    public List<Transform> WayPointList;
    private int _nextWaypoint = 0;

    public override void Execute(IStateMachine stateMachine)
	{
        if (WayPointList == null) {
            throw new Exception($"No Waypoints found in Patrol Action for {stateMachine.GetAgent().gameObject.name}");
        }

		Patrol(stateMachine);
	}

	private void Patrol(IStateMachine stateMachine)
	{
        var navMeshAgent = stateMachine.GetAgent().GetNavMeshAgent();
        var agentStats = stateMachine.GetAgent().GetStats();
        var wayPoints = WayPointList;

        navMeshAgent.stoppingDistance = agentStats.PatrolStoppingDistance;
        navMeshAgent.destination = wayPoints[_nextWaypoint].position;
        navMeshAgent.isStopped = false;

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending) 
		{
            _nextWaypoint = (_nextWaypoint + 1) % WayPointList.Count;
		}
	}
}

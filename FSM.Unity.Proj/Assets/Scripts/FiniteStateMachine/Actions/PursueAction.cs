using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Finite State Machine/Actions/Pursue")]
public class PursueAction : Action {

	public override void Execute(StateMachine controller)
	{
		controller.NavMeshAgent.stoppingDistance = controller.NpcStats.AttackRange;
		controller.NavMeshAgent.destination = controller.Target.position;
		controller.NavMeshAgent.isStopped = false;
	}
}

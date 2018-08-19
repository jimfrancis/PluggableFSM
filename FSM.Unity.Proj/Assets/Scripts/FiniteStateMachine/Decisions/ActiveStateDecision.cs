using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine/Decisions/ActiveState")]
public class ActiveStateDecision : Decision 
{
	public override bool Evaluate(IStateMachine stateMachine)
	{
		return stateMachine.GetAgent().Target.gameObject.activeSelf;
	}
}

using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Collections;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(menuName = "Finite State Machine/Decisions/Range")]
public class RangeDecision : Decision
{
	[HideInInspector] public Stopwatch StopWatch;

	public void OnEnable()
	{
		StopWatch = new Stopwatch();
	}

	public override bool Evaluate(IStateMachine stateMachine)
	{
		return CheckLineOfSight(stateMachine);
	}

	private bool CheckLineOfSight(IStateMachine stateMachine)
	{
		return FireRaycasts(stateMachine);
	}

	private bool FireRaycasts(IStateMachine stateMachine)
	{
        bool hasVisionOfPlayer = false;

        var agentEyes = stateMachine.GetAgent().GetEyes();
        var agentStats = stateMachine.GetAgent().GetStats();

		for (var i = 0; i < 5; i++)
		{
			RaycastHit hit;
			Vector3 rayDirection;
			
			switch (i)
			{
				case 0:
					rayDirection = agentEyes.transform.forward - agentEyes.right;
					break;
				case 1:
					rayDirection = (agentEyes.transform.forward - agentEyes.right / 2);
					break;
				case 2:
					rayDirection = agentEyes.transform.forward;
					break;
				case 3:
					rayDirection = (agentEyes.transform.forward + agentEyes.right / 2);
					break;
				case 4:
					rayDirection = agentEyes.transform.forward + agentEyes.right;
					break;
				default:
					rayDirection = new Vector3();
					break;
			}
			
			Debug.DrawRay(agentEyes.transform.position, rayDirection * agentStats.ViewDistance, stateMachine.GetCurrentState().GetCurrentStateColor());
			var layerMask = 1 << 10;
			
			if (Physics.Raycast(agentEyes.transform.position, rayDirection, out hit, agentStats.ViewDistance, layerMask) 
			    && hit.collider.CompareTag("player"))
			{
                stateMachine.GetAgent().Target = hit.transform;
				StopWatch.Start();
			}
		}

		if (hasVisionOfPlayer)
		{
			var expired = StopWatch.Elapsed.Seconds >= stateMachine.GetAgent().GetStats().VisionDropDuration;
			
			if (expired)
			{
				StopWatch.Reset();
				return false;
			}

			return true;
		}

		return false;
	}
}

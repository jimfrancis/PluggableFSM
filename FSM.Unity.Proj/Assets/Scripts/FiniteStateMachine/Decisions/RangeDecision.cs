using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(menuName = "Finite State Machine/Decisions/Range")]
public class RangeDecision : Decision
{
	[HideInInspector] public Stopwatch StopWatch;
    private bool _hasVisionOfTarget = false;

    void OnEnable()
	{
		StopWatch = new Stopwatch();
	}

	public override bool Evaluate(IStateMachine stateMachine)
	{
		return CheckLineOfSight(stateMachine);
	}

	private bool CheckLineOfSight(IStateMachine stateMachine)
	{
		_hasVisionOfTarget = FireRaycasts(stateMachine);

        if (_hasVisionOfTarget)
        {
            if (!StopWatch.IsRunning) {
                StopWatch.Start();
            }

            var expired = StopWatch.Elapsed.Seconds >= stateMachine.GetAgent().GetStats().VisionDropDuration;

            if (expired)
            {
                StopWatch.Reset();
                _hasVisionOfTarget = FireRaycasts(stateMachine);
                return _hasVisionOfTarget;
            }

            return true;
        }

        if (StopWatch.IsRunning) {
            StopWatch.Reset();
        }

        return false;
    }

	private bool FireRaycasts(IStateMachine stateMachine)
	{
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

            // invert bitmask to collide with all layers except 10
            layerMask = ~layerMask;
			
			if (Physics.Raycast(agentEyes.transform.position, rayDirection, out hit, agentStats.ViewDistance, layerMask) 
			    && hit.collider.CompareTag("agent"))
			{
                stateMachine.GetAgent().Target = hit.transform;
                return true;
			}
		}

        return false;
	}
}

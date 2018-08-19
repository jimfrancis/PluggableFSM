using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine/Decisions/Scan")]
public class ScanDecision : Decision {
    public override bool Evaluate(IStateMachine stateMachine)
    {
        return Scan(stateMachine);
    }

    private bool Scan(IStateMachine stateMachine)
    {
        var agent = stateMachine.GetAgent();
        var agentStats = agent.GetStats();

        agent.GetNavMeshAgent().isStopped = true;
        agent.transform.Rotate(0, agentStats.SearchingTurnSpeed * Time.deltaTime, 0);

        return stateMachine.CheckTimeInState(agentStats.SearchDuration);
    }
}

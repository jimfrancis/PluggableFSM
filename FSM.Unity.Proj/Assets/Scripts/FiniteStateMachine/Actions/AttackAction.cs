using Assets.Scripts.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Finite State Machine/Actions/Attack")]
public class AttackAction : Action {
    
    public override void Execute(IStateMachine stateMachine)
    {
        Attack(stateMachine);
    }

    private void Attack(IStateMachine stateMachine)
    {
        RaycastHit hit;

        var agent = stateMachine.GetAgent();
        var agentEyes = agent.GetEyes();
        var agentStats = agent.GetStats();
        

        Debug.DrawRay(agentEyes.position, agentEyes.forward.normalized * agentStats.AttackRange, Color.red);
        
        if (Physics.Raycast(agentEyes.position,
                agentEyes.forward, out hit, agentStats.AttackRange) 
            && hit.collider.CompareTag("player"))
        {
            agent.DefaultAttack();
        }
    }
}

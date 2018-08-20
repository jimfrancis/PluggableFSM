using Assets.Scripts.FiniteStateMachine;
using Assets.Scripts.FiniteStateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Finite State Machine/States")]
public class State : ScriptableObject, IState
{
    public Action[] Actions;
    public Transition[] Transitions;
    public Color StateGizmoColour = Color.grey;
    
    public void UpdateState(IStateMachine stateMachine)
    {
        ExecuteActions(stateMachine);
        CheckTransitions(stateMachine);
    }

    public Color GetCurrentStateColor() {
        return StateGizmoColour != null ? StateGizmoColour : Color.gray;
    }

    public Transition[] GetTransitions() {
        return Transitions;
    }

    private void ExecuteActions(IStateMachine stateMachine)
    {
        foreach (var action in Actions)
        {
            action.Execute(stateMachine);
        }
    }

    private void CheckTransitions(IStateMachine stateMachine)
    {
        foreach (var transition in Transitions)
        {
            var result = transition.Decision.Evaluate(stateMachine);

            if (result)
            {
                stateMachine.TransitionState(transition.TrueState);
                return;
            } 
            
            stateMachine.TransitionState(transition.FalseState);
        }
    }
}

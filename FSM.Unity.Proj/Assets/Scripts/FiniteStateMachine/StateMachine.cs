using Assets.Scripts.FiniteStateMachine;
using Assets.Scripts.FiniteStateMachine.States;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine: IStateMachine
{
    public IState CurrentState;
    public IState RemainingState;

    private float _timeElapsedInState;
    private bool _isActive;
    private Agent _agent;

    public void Init(Agent self, IState intialState = null) {
        _agent = self;
        CurrentState = intialState;
    }

    public void Start()
    {
        _isActive = true;
        _agent.GetNavMeshAgent().isStopped = false;
        _timeElapsedInState = 0;
    }

    public void Update()
    {
        if (!_isActive)
        {
            return;
        }

        CurrentState.UpdateState(this);
    }

    public void Stop()
    {
        _isActive = false;
        _agent.GetNavMeshAgent().isStopped = true;
        _timeElapsedInState = 0;
    }

    public Agent GetAgent() {
        return _agent;
    }

    public IState GetCurrentState() {
        return CurrentState;
    }

    public void TransitionState(State nextState)
    {
        if (nextState != RemainingState)
        {
            CurrentState = nextState;
            ExitState();
        }
    }

    public bool CheckTimeInState(float duration)
    {
        _timeElapsedInState += Time.deltaTime;
        return (_timeElapsedInState >= duration);
    }

    private void ExitState()
    {
        _timeElapsedInState = 0;
    }
}

using Assets.Scripts.FiniteStateMachine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine
{
    public interface IStateMachine
    {
        void Init(Agent self, IState initialState = null);
        void Start();
        void Update();
        void Stop();
        void TransitionState(State nextState);

        bool CheckTimeInState(float duration);

        Agent GetAgent();
        IState GetCurrentState();
    }
}

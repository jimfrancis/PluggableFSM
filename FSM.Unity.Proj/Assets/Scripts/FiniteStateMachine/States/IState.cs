using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine.States
{
    public interface IState
    {
        void UpdateState(IStateMachine stateMachine);
        Color GetCurrentStateColor();
        Transition[] GetTransitions();
    }
}

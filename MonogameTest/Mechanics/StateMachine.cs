using MonogameTest.Actors.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest
{
    internal class StateMachine
    {
        public State PreviousState;
        public State CurrentState;

        public StateMachine(State InitialState) 
        {
            CurrentState = InitialState;
        }

        public void TransitionTo(State state) 
        {
            if(CurrentState == state)
            {
                return;
            }

            PreviousState = CurrentState;
            CurrentState = state;
            PreviousState.Exit();
            CurrentState.Enter();
        }
    }
}

using System;
using System.Collections.Generic;
using FSM;
using FSM.States;

namespace Platformer.FSM
{
    public class CharacterStateMachine
    {
        public Dictionary<Type, ICharacterState> States;
        private ICharacterState _currentState;

        public CharacterStateMachine()
        {
            States = new Dictionary<Type, ICharacterState>()
            {
                [typeof(IdleCharacterState)] = new IdleCharacterState(this),
                [typeof(RunCharacterState)] = new RunCharacterState(this),
                [typeof(JumpCharacterState)] = new JumpCharacterState(this)
            };
        }

        public void EnterIn<TState>() where TState : ICharacterState
        {
            if (States.TryGetValue(typeof(TState), out ICharacterState state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }
    }
}

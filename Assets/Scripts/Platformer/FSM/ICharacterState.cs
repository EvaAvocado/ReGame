using System;

namespace FSM
{
    public interface ICharacterState
    {
        event Action EnterHandler;
        void Enter();
        void Exit();
    }
}

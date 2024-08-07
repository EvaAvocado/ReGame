using System;
using UnityEngine;

namespace FSM
{
    public class CharacterState : ICharacterState
    {
        public event Action EnterHandler = delegate {};
            
        public void Enter()
        {
            EnterHandler();
        }

        public void Exit()
        {
            //Exit
        }
    }
}
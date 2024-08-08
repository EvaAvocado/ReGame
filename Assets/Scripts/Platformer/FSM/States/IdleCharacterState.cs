using Platformer.FSM;

namespace FSM.States
{
    public class IdleCharacterState : CharacterState
    {
        private CharacterStateMachine _characterStateMachine;
        
        public IdleCharacterState(CharacterStateMachine characterStateMachine)
        {
            _characterStateMachine = characterStateMachine;
        }
    }
}
using Platformer.FSM;

namespace FSM.States
{
    public class JumpCharacterState : CharacterState
    {
        private CharacterStateMachine _characterStateMachine;
        
        public JumpCharacterState(CharacterStateMachine characterStateMachine)
        {
            _characterStateMachine = characterStateMachine;
        }
    }
}
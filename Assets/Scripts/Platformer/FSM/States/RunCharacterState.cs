using Platformer.FSM;

namespace FSM.States
{
    public class RunCharacterState : CharacterState
    {
        private CharacterStateMachine _characterStateMachine;
        
        public RunCharacterState(CharacterStateMachine characterStateMachine)
        {
            _characterStateMachine = characterStateMachine;
        }
    }
}
namespace StateMachine
{
    public class BaseState
    {
        protected GameStateMachine gameStateMachine;

        public BaseState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public virtual void Enter() {}
        public virtual void Exit(){}
    }
}
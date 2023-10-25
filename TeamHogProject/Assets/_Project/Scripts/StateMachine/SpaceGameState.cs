using PreparationState;
using SpaceGameState;
using UnityEngine;

namespace StateMachine
{
    public class SpaceGameState : BaseState
    {
        private SpaceGameManager _spaceGameManager;
        private CharactersManager _charactersManager;
        private BackgroundManager _backgroundManager;
        public SpaceGameState(GameStateMachine gameStateMachine, SpaceGameManager spaceGameManager, CharactersManager charactersManager, BackgroundManager backgroundManager) : base(gameStateMachine)
        {
            _spaceGameManager = spaceGameManager;
            _charactersManager = charactersManager;
            _backgroundManager = backgroundManager;
        }

        public override void Enter()
        {
            _backgroundManager.StartMoveBack();
            _spaceGameManager.ChangeRocketState(true);
            _spaceGameManager.EnterState();
            DebugLogger.SendMessage("Enter space game state", Color.green);
        }

        public override void Exit()
        {
            _backgroundManager.SetDefaultScreen();
            _charactersManager.RemoveAll();
            _spaceGameManager.DestroyRocket();
            _spaceGameManager.ExitState();
            DebugLogger.SendMessage("Exit space game state", Color.red);
        }
    }
}
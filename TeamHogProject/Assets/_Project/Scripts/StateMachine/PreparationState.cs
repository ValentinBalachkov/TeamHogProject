using PreparationState;
using SpaceGameState;
using UnityEngine;

namespace StateMachine
{
    public class PreparationState : BaseState
    {
        private TaskManager _taskManager;
        private SpaceGameManager _spaceGameManager;
        private BackgroundManager _backgroundManager;
        public PreparationState(GameStateMachine gameStateMachine, TaskManager taskManager, SpaceGameManager spaceGameManager, BackgroundManager backgroundManager) : base(gameStateMachine)
        {
            _taskManager = taskManager;
            _spaceGameManager = spaceGameManager;
            _backgroundManager = backgroundManager;
        }

        public override void Enter()
        {
            _spaceGameManager.SpawnRocket();
            _taskManager.StartTaskManager();
            _spaceGameManager.ChangeRocketState(false);
            DebugLogger.SendMessage("Enter preparation state", Color.green);
        }

        public override void Exit()
        {
            _taskManager.StopTaskManager();
            DebugLogger.SendMessage("Exit preparation state", Color.red);
        }
    }
}
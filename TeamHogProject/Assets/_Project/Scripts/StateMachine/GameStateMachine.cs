using System.Collections.Generic;
using System.Linq;
using PreparationState;
using SpaceGameState;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        private BaseState _currentState;

        private PreparationState _preparationState;
        private SpaceGameState _spaceGameState;

        private List<BaseState> _states = new();

        private TaskManager _taskManager;
        private SpaceGameManager _spaceGameManager;
        private CharactersManager _charactersManager;
        private BackgroundManager _backgroundManager;

        [Inject]
        private void Init(TaskManager taskManager, SpaceGameManager spaceGameManager,
            CharactersManager charactersManager, BackgroundManager backgroundManager)
        {
            _taskManager = taskManager;
            _spaceGameManager = spaceGameManager;
            _charactersManager = charactersManager;
            _backgroundManager = backgroundManager;
        }

        private void Awake()
        {
            _preparationState = new PreparationState(this, _taskManager, _spaceGameManager, _backgroundManager);
            _spaceGameState = new SpaceGameState(this, _spaceGameManager, _charactersManager, _backgroundManager);

            _states.Add(_preparationState);
            _states.Add(_spaceGameState);
        }

        private void Start()
        {
            ChangeState<PreparationState>();
        }

        public void ChangeState<T>() where T : BaseState
        {
            DebugLogger.SendMessage($"{_currentState}", Color.cyan);
            _currentState?.Exit();
            DebugLogger.SendMessage("2", Color.cyan);
            _currentState = _states.FirstOrDefault(x => x is T);
            DebugLogger.SendMessage("3", Color.cyan);
            _currentState.Enter();
            DebugLogger.SendMessage("4", Color.cyan);
        }
    }
}
using SpaceGameState;
using StateMachine;
using UniRx;
using UnityEngine;
using Zenject;

namespace PreparationState
{
    public class TaskManager : MonoBehaviour
    {
        private InteractiveObjectsPool _interactiveObjectsPool;
        private CharactersManager _charactersManager;
        private RocketManager _rocketManager;
        private GameStateMachine _gameStateMachine;
        private SpaceGameManager _spaceGameManager;
        
        [Inject]
        public void Init(InteractiveObjectsPool interactiveObjectsPool, CharactersManager charactersManager, RocketManager rocketManager, GameStateMachine gameStateMachine, SpaceGameManager spaceGameManager)
        {
            _interactiveObjectsPool = interactiveObjectsPool;
            _charactersManager = charactersManager;
            _rocketManager = rocketManager;
            _gameStateMachine = gameStateMachine;
            _spaceGameManager = spaceGameManager;
        }

        public void StartTaskManager()
        {
            _interactiveObjectsPool.SpawnObjects();
            _charactersManager.SpawnObjects();
            AddOnClickListeners();
        }

        public void StopTaskManager()
        {
            
            RemoveOnClickListeners();
           
            _interactiveObjectsPool.DestroyAll();
           
        }

        private void AddOnClickListeners()
        {
            foreach (var item in  _interactiveObjectsPool.InteractiveObjectControllers)
            {
                item.OnClickOnbject += _rocketManager.SetDetail;
            }

            _rocketManager.AddListenerToDict(OnDetailAdd);
        }

        private void RemoveOnClickListeners()
        {
            foreach (var item in  _interactiveObjectsPool.InteractiveObjectControllers)
            {
                item.OnClickOnbject -= _rocketManager.SetDetail;
            }
            
            _rocketManager.ClearListenersDict();
        }

        private void OnDetailAdd(DictionaryAddEvent<RocketDetailsEnum, InteractiveObjectData> rocketDetail)
        {
            _spaceGameManager.RocketController.SetSprite(rocketDetail.Key, rocketDetail.Value);
            DebugLogger.SendMessage($"{rocketDetail.Key}", Color.red);
            _interactiveObjectsPool.CanTake = true;
            if (rocketDetail.Key == RocketDetailsEnum.Up)
            {
                _spaceGameManager.RocketController.SetDefaultUp();
                DebugLogger.SendMessage($"change state", Color.red);
                _gameStateMachine.ChangeState<StateMachine.SpaceGameState>();
            }
        }
        
    }
}
using System.Collections;
using AddressablesPlugin;
using PreparationState;
using StateMachine;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpaceGameState
{
    public class SpaceGameManager : MonoBehaviour
    {
        public RocketController RocketController => _rocketController;
        
        [SerializeField] private Transform _rocketSpawnPoint;
        [SerializeField] private HpBarController _hpBarController;
        [SerializeField] private TMP_Text _counter;
        [SerializeField] private GameObject _fuelText;
        
        
        

        private AddressablesController _addressablesController;
        private ZenjectFactory _zenjectFactory;
        private EnemyManager _enemyManager;

        private RocketController _rocketController;
        private GameStateMachine _gameStateMachine;
        private BackgroundManager _backgroundManager;
        private RocketManager _rocketManager;

        private const int DISTANCE = 2000;


        [Inject]
        private void Init(AddressablesController addressablesController, ZenjectFactory zenjectFactory, EnemyManager enemyManager, GameStateMachine gameStateMachine, BackgroundManager backgroundManager, RocketManager rocketManager)
        {
            _addressablesController = addressablesController;
            _zenjectFactory = zenjectFactory;
            _enemyManager = enemyManager;
            _gameStateMachine = gameStateMachine;
            _backgroundManager = backgroundManager;
            _rocketManager = rocketManager;
        }

        private IEnumerator TimerCoroutine()
        {
            int distance = 0;

            float endDistance = _rocketController.VelocityTimeValue * DISTANCE;
            
            while (distance < endDistance)
            {
                yield return new WaitForSeconds(0.01f);
                distance++;
                _counter.text = $"{distance}/{(int)endDistance}";
            }
            _enemyManager.StopEnemySpawn();
            _backgroundManager.EndGameBack(_gameStateMachine);
        }

        public void EnterState()
        {
            StartCoroutine(TimerCoroutine());
            _enemyManager.StartEnemySpawn();
        }

        public void ExitState()
        {
            _counter.text = "";
            StopAllCoroutines();
            _enemyManager.StopEnemySpawn();
        }

        public void SpawnRocket()
        {
            _rocketController = _zenjectFactory.CreateRocket(_rocketSpawnPoint);
            DebugLogger.SendMessage($"{_fuelText}", Color.red);
            _rocketController.Init(_addressablesController.RocketData, _hpBarController, _gameStateMachine, _fuelText);
        }

        public void DestroyRocket()
        {
            _rocketController.ClearListeners();
            Destroy(_rocketController.gameObject);
        }

        public void ChangeRocketState(bool isSpaceGameState)
        {
            _rocketController.IsSpaceGameState = isSpaceGameState;
            if (isSpaceGameState)
            {
                _rocketController.SetParametersItem(_rocketManager.rocketDetailsDict); 
            }
        }
        
    }
}
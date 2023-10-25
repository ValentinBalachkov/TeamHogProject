using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AddressablesPlugin;
using PreparationState;
using UnityEngine;
using Zenject;

namespace SpaceGameState
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;

        private AddressablesController _addressablesController;
        private ZenjectFactory _zenjectFactory;

        private const int TIMER = 2;
        private List<EnemyData> _currentPool = new();
        private List<EnemyController> _enemyControllers = new();
        private SpaceGameManager _spaceGameManager;

        [Inject]
        private void Init(AddressablesController addressablesController, ZenjectFactory zenjectFactory, SpaceGameManager spaceGameManager)
        {
            _addressablesController = addressablesController;
            _zenjectFactory = zenjectFactory;
            _spaceGameManager = spaceGameManager;
        }

        public void StartEnemySpawn()
        {
            StartCoroutine(SpawnEnemyCoroutine());
        }

        public void StopEnemySpawn()
        {
            foreach (var enemy in _enemyControllers)
            {
                enemy.OnDestroy = null;
                Destroy(enemy.gameObject);
            }

            _enemyControllers.Clear();
            StopAllCoroutines();
        }

        private IEnumerator SpawnEnemyCoroutine()
        {
            yield return new WaitForSeconds(TIMER);
            
            List<EnemyData> enemy = new(_addressablesController.EnemyData);

            bool addFirstEnemy = CheckChance(50);
            bool addSecondEnemy = CheckChance(25);
            bool addThirdEnemy = CheckChance(10);

            if (addFirstEnemy)
            {
                AddEnemyOnPool(enemy);
            }

            if (addSecondEnemy)
            {
                AddEnemyOnPool(enemy);
            }

            if (addThirdEnemy)
            {
                AddEnemyOnPool(enemy);
            }

            List<Transform> currentPoints = new(_points);

            foreach (var data in _currentPool)
            {
                int index = Random.Range(0, currentPoints.Count);
                DebugLogger.SendMessage($"{index}, {currentPoints.Count}", Color.blue);
                var point = currentPoints[index];
                currentPoints.RemoveAt(index);
                var enemyController = _zenjectFactory.CreateEnemy(point);
                enemyController.Init(data, _spaceGameManager);
                enemyController.OnDestroy += OnEnemyDestroy;
                _enemyControllers.Add(enemyController);
            }

            _currentPool.Clear();
            currentPoints.Clear();
            enemy.Clear();

            
            StartCoroutine(SpawnEnemyCoroutine());
        }

        private void OnEnemyDestroy(EnemyController enemyController)
        {
            enemyController.OnDestroy -= OnEnemyDestroy;
            _enemyControllers.Remove(enemyController);
            Destroy(enemyController.gameObject);
        }

        private void AddEnemyOnPool(List<EnemyData> enemy)
        {
            bool isStaticEnemy = CheckChance(75);
            int typeId = isStaticEnemy ? 0 : 1;
            var typeList = enemy.Where(x => x.EnemyType == (EnemyType)typeId).ToList();
            var item = typeList[Random.Range(0, typeList.Count)];
            _currentPool.Add(item);
        }

        private bool CheckChance(int chance)
        {
            int enemyChance = Random.Range(0, 101);
            return enemyChance < chance;
        }
    }
}
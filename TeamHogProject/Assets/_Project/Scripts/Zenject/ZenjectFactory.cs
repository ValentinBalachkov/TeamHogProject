using AddressablesPlugin;
using SpaceGameState;
using UnityEngine;
using Zenject;

namespace PreparationState
{
    public class ZenjectFactory
    {
        private GameObject _interactiveObjectPrefab;
        private GameObject _characterPrefab;
        private GameObject _rocketPrefab;
        private GameObject _enemyPrefab;
        private DiContainer _diContainer;
        
        private const string CharacterPrefabLabel = "CharacterPrefab";
        private const string InteractiveObjectPrefabLabel = "InteractiveObjectPrefab";
        private const string RocketPrefabLabel = "RocketPrefab";
        private const string EnemyPrefabLabel = "EnemyPrefab";

        public ZenjectFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void LoadAll()
        {
            _interactiveObjectPrefab = AddressablesLoader.InitAsset<GameObject>(InteractiveObjectPrefabLabel);
            _characterPrefab = AddressablesLoader.InitAsset<GameObject>(CharacterPrefabLabel);
            _rocketPrefab = AddressablesLoader.InitAsset<GameObject>(RocketPrefabLabel);
            _enemyPrefab = AddressablesLoader.InitAsset<GameObject>(EnemyPrefabLabel);
        }

        public CharacterController CreateCharacter(Transform at)
        {
            return _diContainer.InstantiatePrefab(_characterPrefab, at.position, Quaternion.identity, at).GetComponent<CharacterController>();
        }
        public InteractiveObjectController CreateInteractiveObject(Transform at)
        {
           return _diContainer.InstantiatePrefab(_interactiveObjectPrefab, at.position, Quaternion.identity, at).GetComponent<InteractiveObjectController>();
        }
        
        public RocketController CreateRocket(Transform at)
        {
            return _diContainer.InstantiatePrefab(_rocketPrefab, at.position, Quaternion.identity, at).GetComponent<RocketController>();
        }
        
        public EnemyController CreateEnemy(Transform at)
        {
            return _diContainer.InstantiatePrefab(_enemyPrefab, at.position, Quaternion.identity, at).GetComponent<EnemyController>();
        }
    }
}
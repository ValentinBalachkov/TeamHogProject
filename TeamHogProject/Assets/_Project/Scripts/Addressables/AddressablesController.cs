using System.Collections.Generic;
using PreparationState;
using SpaceGameState;
using UnityEngine;

namespace AddressablesPlugin
{
    public class AddressablesController : MonoBehaviour
    {
        public List<CharacterData> CharactersData => _charactersData;
        public List<InteractiveObjectData> InteractiveObjectsData => _interactiveObjectsData;
        public List<EnemyData> EnemyData => _enemyData;
        public RocketData RocketData => _rocketData;

        private List<CharacterData> _charactersData = new();
        private List<InteractiveObjectData> _interactiveObjectsData = new();
        private List<EnemyData> _enemyData = new();
        private RocketData _rocketData;

        private void Awake()
        {
            Instantiate();
        }

        private void Instantiate()
        {
            _charactersData = AddressablesLoader.InitAssets<CharacterData>("Character");
            _interactiveObjectsData = AddressablesLoader.InitAssets<InteractiveObjectData>("InteractiveObject");
            _enemyData = AddressablesLoader.InitAssets<EnemyData>("Enemy");
            _rocketData = AddressablesLoader.InitAsset<RocketData>("Rocket");
        }
    }
}
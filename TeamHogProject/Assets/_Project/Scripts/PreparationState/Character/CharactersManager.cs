using System.Collections.Generic;
using AddressablesPlugin;
using SpaceGameState;
using UnityEngine;
using Zenject;

namespace PreparationState
{
    public class CharactersManager : MonoBehaviour
    {
        public List<CharacterController> CharacterControllers => _characterControllers;
        [SerializeField] private Transform[] _points;

        private AddressablesController _addressablesController;
        private ZenjectFactory _zenjectFactory;
        private SpaceGameManager _spaceGameManager;
        private List<CharacterController> _characterControllers = new();
        

        [Inject]
        private void Init(AddressablesController addressablesController, ZenjectFactory zenjectFactory, SpaceGameManager spaceGameManager)
        {
            _addressablesController = addressablesController;
            _zenjectFactory = zenjectFactory;
            _spaceGameManager = spaceGameManager;
        }

        public void SpawnObjects()
        {
            for (int i = 0; i < _points.Length; i++)
            {
                var item = _zenjectFactory.CreateCharacter(_points[i]);
                item.Init(_addressablesController.CharactersData[i], _spaceGameManager.RocketController);
                _characterControllers.Add(item);
            }
        }

        public void RemoveAll()
        {
            foreach (var character in _characterControllers)
            {
                Destroy(character.gameObject);
            }
            _characterControllers.Clear();
        }
    }
}
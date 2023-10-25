using System.Collections.Generic;
using System.Linq;
using AddressablesPlugin;
using UnityEngine;
using Zenject;


namespace PreparationState
{
    public class InteractiveObjectsPool : MonoBehaviour
    {
        public bool CanTake = true;
        public List<InteractiveObjectController> InteractiveObjectControllers => _interactiveObjectControllers;
        
        [SerializeField] private Transform[] _points;

        private AddressablesController _addressablesController;
        private ZenjectFactory _zenjectFactory;
        private List<InteractiveObjectController> _interactiveObjectControllers = new();
        
        [Inject]
        private void Init(AddressablesController addressablesController, ZenjectFactory zenjectFactory)
        {
            _addressablesController = addressablesController;
            _zenjectFactory = zenjectFactory;
        }

        public void SpawnObjects()
        {
            List<InteractiveObjectData> _interactiveObjects = new(_addressablesController.InteractiveObjectsData);
            List<InteractiveObjectData> _interactiveObjectsPool = new();

            int startIndex = 1;
            int endIndex = 3;
            
            for (int i = 0; i < 3; i++)
            {
                var index = Random.Range(startIndex, endIndex + 1);
                var item = _interactiveObjects.FirstOrDefault(x => x.ID == index);
                _interactiveObjectsPool.Add(item);
                _interactiveObjects.Remove(item);
                startIndex += 3;
                endIndex += 3;
            }

            for (int i = 0; i < 2; i++)
            {
                var index = Random.Range(0, _interactiveObjects.Count);
                var item = _interactiveObjects[index];
                _interactiveObjectsPool.Add(item);
                _interactiveObjects.Remove(item);
            }

            for (int i = 0; i < _interactiveObjectsPool.Count; i++)
            {
                var item = _zenjectFactory.CreateInteractiveObject(_points[i]);
               item.Init(_interactiveObjectsPool[i], this);
               DebugLogger.SendMessage($"{_interactiveObjectsPool[i].ItemName} added on scene", Color.yellow);
                _interactiveObjectControllers.Add(item);
            }
        }

        public void DestroyAll()
        {
            foreach (var item in _interactiveObjectControllers)
            {
                Destroy(item.gameObject);
            }
            _interactiveObjectControllers.Clear();
        }
    }
}


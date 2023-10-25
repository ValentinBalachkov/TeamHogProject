using System;
using UnityEngine;

namespace PreparationState
{
    public class InteractiveObjectController : MonoBehaviour
    {
        public Action<InteractiveObjectController> OnClickOnbject;
        public InteractiveObjectData InteractiveObjectData => _interactiveObjectData;

        private InteractiveObjectData _interactiveObjectData;

        private InteractiveObjectsPool _interactiveObjectsPool;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        


        public void Init(InteractiveObjectData interactiveObjectData, InteractiveObjectsPool interactiveObjectsPool)
        {
            _interactiveObjectData = interactiveObjectData;
            _interactiveObjectsPool = interactiveObjectsPool;
            _spriteRenderer.sprite = interactiveObjectData.Sprite[0];
            DebugLogger.SendMessage($"{_interactiveObjectsPool}", Color.blue);
        }
        private void OnMouseDown()
        {
            if (_interactiveObjectsPool == null)
            {
                return;
            }
            if (_interactiveObjectsPool.CanTake)
            {
                DebugLogger.SendMessage("Click on obj", Color.cyan);
                OnClickOnbject?.Invoke(this);
                _interactiveObjectsPool.CanTake = false;
            }
        }
    }
}
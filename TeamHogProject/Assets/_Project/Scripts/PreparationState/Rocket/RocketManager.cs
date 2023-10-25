using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace PreparationState
{
    public class RocketManager : MonoBehaviour
    {
        public ReactiveDictionary<RocketDetailsEnum, InteractiveObjectData> rocketDetailsDict = new();
        private int _currentNumber;
        private CompositeDisposable _disposable = new();
        private CharactersManager _charactersManager;

        [Inject]
        public void Init(CharactersManager charactersManager)
        {
            _charactersManager = charactersManager;
        }

        public void AddListenerToDict(Action<DictionaryAddEvent<RocketDetailsEnum, InteractiveObjectData>> action)
        {
            rocketDetailsDict.Clear();
            _disposable.Clear();
            rocketDetailsDict.ObserveAdd().Subscribe(action).AddTo(_disposable);
        }

        public void ClearListenersDict()
        {
            _currentNumber = 0;
        }

        public void SetDetail(InteractiveObjectController objectController)
        {
            _currentNumber++;
            var character = _charactersManager.CharacterControllers.FirstOrDefault(x =>
                x.CharacterData.State == (RocketDetailsEnum)_currentNumber);
            character?.GetItemAnimation(objectController, () =>
            {
                rocketDetailsDict.Add((RocketDetailsEnum)_currentNumber, objectController.InteractiveObjectData);
            });
            
        }
    }
}
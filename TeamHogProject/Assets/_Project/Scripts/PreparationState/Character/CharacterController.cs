using System;
using DG.Tweening;
using SpaceGameState;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PreparationState
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterData CharacterData => _characterData;

        [SerializeField] private Animator _animator;
        
        
        private CharacterData _characterData;


        private RocketController _rocketController;
        public void Init(CharacterData characterData, RocketController rocketController)
        {
            _characterData = characterData;
            _rocketController = rocketController;
        }

        public void SetRun()
        {
            _animator.SetBool("IsRun", true);
        }
        
        public void SetTakeItem()
        {
            _animator.SetBool("IsTakeItem", true);
        }
        
        public void SetGameState()
        {
            _animator.SetBool("IsGame", true);
        }

        public void GetItemAnimation(InteractiveObjectController objectController, Action action)
        {
            SetRun();
            var sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOMove(objectController.transform.position, 1f));

            sequence.OnComplete(() =>
            {
                SetTakeItem();
                objectController.transform.SetParent(transform);
                
                var sequence2 = DOTween.Sequence();
                
                sequence2.Append(transform.DOMove( new Vector3(_rocketController.transform.position.x + Random.Range(-0.5f, 0.5f), _rocketController.transform.position.y + Random.Range(0, 1f), 0) , 1f));

                sequence2.OnComplete(() =>
                {
                    SetGameState();
                    transform.SetParent(_rocketController.transform);
                    objectController.gameObject.SetActive(false);
                    action?.Invoke();
                    DebugLogger.SendMessage("CompleteANim", Color.green);
                    action = null;
                    sequence2.Kill();
                    sequence.Kill();
                });
                
                
            });
        }
        
    }
}
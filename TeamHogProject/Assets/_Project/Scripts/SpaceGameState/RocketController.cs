using System;
using DG.Tweening;
using PreparationState;
using StateMachine;
using UniRx;
using UnityEngine;

namespace SpaceGameState
{
    public class RocketController : MonoBehaviour
    {
        public bool IsSpaceGameState
        {
            get => _isSpaceGameState;
            set => _isSpaceGameState = value;
        }


        public float VelocityTimeValue => _velocityTimeValue;
        
        public ReactiveProperty<float> RocketVelocity = new();

        public ReactiveProperty<float> RocketFuel = new();

        public ReactiveProperty<float> RocketManeurity = new();

        private CompositeDisposable _disposableVelocity = new();

        private CompositeDisposable _disposableFuel = new();

        private CompositeDisposable _disposableManeurity = new();

        [SerializeField] private SpriteRenderer _upSprite;
        [SerializeField] private SpriteRenderer _downSprite;
        [SerializeField] private SpriteRenderer _middleSprite;
        [SerializeField] private SpriteRenderer _defaultUpSpriteRenderer;
        [SerializeField] private GameObject _fire;
        

        [SerializeField] private Sprite _defaultSprite;




        [SerializeField] private Rigidbody2D _rigidbody2D;

        private RocketData _rocketData;

        private bool _isSpaceGameState;

        private Camera _camera;

        private HpBarController _hpBarController;
        private GameStateMachine _gameStateMachine;

        private float _velocityTimeValue;

        private Sequence _sequence1;
        private Sequence _sequence2;
        private Sequence _sequence3;
        private Sequence _sequence4;

        private GameObject _fuelText;


        private bool _haveFuel;

        public void Init(RocketData rocketData, HpBarController hpBarController, GameStateMachine gameStateMachine, GameObject fuelText)
        {
            _gameStateMachine = gameStateMachine;
            _rocketData = rocketData;
            _hpBarController = hpBarController;
            RocketVelocity.Subscribe(OnChangeVelocity).AddTo(_disposableVelocity);
            RocketFuel.Subscribe(OnChangeFuel).AddTo(_disposableFuel);
            RocketManeurity.Subscribe(OnChangeManeurity).AddTo(_disposableManeurity);

            RocketVelocity.Value = rocketData.Velocity;
            RocketFuel.Value = rocketData.Fuel;
            RocketManeurity.Value = rocketData.Maneurity;
            _fuelText = fuelText;
            _camera = Camera.main;
        }

        public void SetSprite(RocketDetailsEnum rocketDetailsEnum, InteractiveObjectData interactiveObjectData)
        {
            switch (rocketDetailsEnum)
            {
                case RocketDetailsEnum.Down:
                    _downSprite.sprite = interactiveObjectData.Sprite[0];
                    break;
                case RocketDetailsEnum.Middle:
                    _middleSprite.sprite = interactiveObjectData.Sprite[0];
                    break;
                case RocketDetailsEnum.Up:
                    _upSprite.sprite = interactiveObjectData.Sprite[0];
                    break;
            }
        }

        public void SetDefaultUp()
        {
            _sequence1 = DOTween.Sequence();
            _sequence2 = DOTween.Sequence();
            _sequence3 = DOTween.Sequence();
            _sequence1.Append(_upSprite.transform.DOShakePosition(0.12f, 0.5f).SetLoops(-1));
            _sequence2.Append(_middleSprite.transform.DOShakePosition(0.09f, 0.5f).SetLoops(-1));
            _sequence3.Append(_downSprite.transform.DOShakePosition(0.1f, 0.5f).SetLoops(-1));

            _fire.SetActive(true);
            _sequence4.Append(_fire.transform.DOShakePosition(0.1f, 0.1f).SetLoops(-1));
            _defaultUpSpriteRenderer.gameObject.SetActive(true);
        }

        private void ClearSprites()
        {
            _fuelText.SetActive(false);
            _sequence1.Kill();
            _sequence2.Kill();
            _sequence3.Kill();
            _sequence4.Kill();
            _defaultUpSpriteRenderer.gameObject.SetActive(false);
            _fire.SetActive(true);
            _downSprite.sprite = _defaultSprite;
            _middleSprite.sprite = _defaultSprite;
            _upSprite.sprite = _defaultSprite;
        }

        public void SetParametersItem(ReactiveDictionary<RocketDetailsEnum, InteractiveObjectData> rocketDetailsDict)
        {
            foreach (var item in rocketDetailsDict)
            {
                var percent = (1 + ((float)item.Value.Velocity / 100));
                RocketVelocity.Value *= percent;
                
                DebugLogger.SendMessage($"{percent}, {RocketVelocity.Value}", Color.blue);
                
                _velocityTimeValue = 1 - ((float)item.Value.Velocity / 100);
                _velocityTimeValue *= percent;
                
                percent = 1 + (float)item.Value.Maneurity / 100;
                RocketManeurity.Value *= percent;
                
                percent = 1 + (float)item.Value.Fuel / 100;
                RocketFuel.Value *= percent;
            }

            _haveFuel = true;
        }

        public void ClearListeners()
        {
            
            ClearSprites();
            _disposableVelocity.Clear();
            _disposableFuel.Clear();
            _disposableManeurity.Clear();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyController enemyController;
            if (collision.TryGetComponent(out enemyController))
            {
                _gameStateMachine.ChangeState<StateMachine.PreparationState>();
            }
        }

        private void FixedUpdate()
        {
            if (!_isSpaceGameState || !_haveFuel)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }

            var position = _camera.WorldToViewportPoint(transform.localPosition);
            float move = Input.GetAxis("Horizontal");

            if (move != 0)
            {
                RocketFuel.Value -= 0.1f;
            }

            if (position.x > 0.9f && move > 0)
            {
                move = 0;
            }
            else if(position.x < 0.1f && move < 0)
            {
                move = 0; 
            }
            _rigidbody2D.velocity = new Vector2(move * RocketManeurity.Value, 0);
        }

        private void OnChangeVelocity(float velocity)
        {
            
        }
        
        private void OnChangeFuel(float fuel)
        {
            if (fuel <= 0)
            {
                if (_fuelText != null)
                {
                    _fuelText.SetActive(true);
                }
                
                _haveFuel = false;
            }
        }
        
        private void OnChangeManeurity(float maneurity)
        {
            
        }
        
    }
}
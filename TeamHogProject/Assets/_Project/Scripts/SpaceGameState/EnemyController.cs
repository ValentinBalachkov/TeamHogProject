using System;
using PreparationState;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceGameState
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyData EnemyData => _enemyData;
        public Action<EnemyController> OnDestroy;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        

        private EnemyData _enemyData;

        private float _currentVelocity;

        private float _angle;


        private Camera _camera;

        public void Init(EnemyData enemyData, SpaceGameManager spaceGameManager)
        {
            _enemyData = enemyData;

            if (enemyData.EnemyType == EnemyType.Dynamic)
            {
                var path = GetComponentInParent<EnemyPathData>().RoadNumber;

                if (path == 1 || path == 2)
                {
                    _angle = Random.Range(2f, 7f);
                }
                else if (path == 3)
                {
                    _angle = Random.Range(-7f, 7f);
                }
                else
                {
                    _angle = Random.Range(-7f, -2f);  
                }

                
            }
            else
            {
                _angle = 0;
            }
            
            transform.localEulerAngles = new Vector3(0,0, _enemyData.Angle);
            _camera = Camera.main;
            _currentVelocity = _enemyData.Velocity * spaceGameManager.RocketController.RocketVelocity.Value;
            _spriteRenderer.sprite = enemyData.Sprite;
            DebugLogger.SendMessage($"{spaceGameManager.RocketController.RocketVelocity.Value}", Color.magenta);
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(_angle, -_currentVelocity);

            var position = _camera.WorldToViewportPoint(transform.localPosition);
            

            if (position.x > 1.5f || position.x < -1.5f || position.y < -1.5f)
            {
                OnDestroy?.Invoke(this);
            }
        }
    }
}
using UnityEngine;

namespace SpaceGameState
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public float Angle => _angle;
        public float WaitingTime => _waitingTime;
        public float Velocity => _velocity;
        public Sprite Sprite => _sprite;
        public EnemyType EnemyType => _enemyType;

        [SerializeField] private float  _angle;
        [SerializeField] private float  _waitingTime;
        [SerializeField] private float  _velocity;
        [SerializeField] private Sprite  _sprite;
        [SerializeField] private EnemyType _enemyType;
        
    }
}
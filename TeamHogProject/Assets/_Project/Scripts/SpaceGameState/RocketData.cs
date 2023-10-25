using UnityEngine;

namespace SpaceGameState
{
    [CreateAssetMenu(fileName = "RocketData", menuName = "Data/RocketData")]
    public class RocketData : ScriptableObject
    {
        public int Maneurity => _maneurity;
        public int Fuel => _fuel;
        public int Velocity => _velocity;
        
        [SerializeField] private int _maneurity;
        [SerializeField] private int _fuel;
        [SerializeField] private int _velocity;
    }
}
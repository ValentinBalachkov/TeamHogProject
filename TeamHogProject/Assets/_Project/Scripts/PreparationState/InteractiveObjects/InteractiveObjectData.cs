using UnityEngine;

namespace PreparationState
{
    [CreateAssetMenu(fileName = "InteractiveObjectData", menuName = "Data/InteractiveObjectData")]
    public class InteractiveObjectData : ScriptableObject
    {
        public int ID => _id;
        public Sprite[] Sprite => _sprite;
        public string ItemName => _itemName;
        public string Description => _description;
        public int Maneurity => _maneurity;
        public int Velocity => _velocity;
        public int Fuel => _fuel;

        [SerializeField] private int _id;
        [SerializeField] private Sprite[] _sprite;
        [SerializeField] private string _itemName;
        [SerializeField] private string _description;
        [SerializeField] private int _maneurity;
        [SerializeField] private int _velocity;
        [SerializeField] private int _fuel;
    }
}
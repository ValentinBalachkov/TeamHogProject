using UnityEngine;

namespace PreparationState
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public RocketDetailsEnum State => _state;
        
       [SerializeField] private RocketDetailsEnum _state;
    }
}


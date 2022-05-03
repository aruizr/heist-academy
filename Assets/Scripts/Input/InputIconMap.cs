using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Input
{
    [CreateAssetMenu(fileName = nameof(InputIconMap), menuName = "Trashy Games/Input/Icon Map", order = 0)]
    public class InputIconMap : ScriptableObject
    {
        [SerializeField] private Sprite defaultIcon;
        [SerializeField] private SerializableDictionary<InputAction, Sprite> map;

        public Sprite GetIcon(string path)
        {
            foreach (var pair in map.Where(pair => pair.Key.bindings[0].path.Equals(path))) return pair.Value;

            return defaultIcon;
        }
    }
}
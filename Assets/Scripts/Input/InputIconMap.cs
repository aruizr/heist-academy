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
            foreach (var pair in from pair in map
                let bindings = pair.Key.bindings
                where bindings.Count != 0
                where bindings[0].path.Equals(path)
                select pair) return pair.Value;

            return defaultIcon;
        }
    }
}
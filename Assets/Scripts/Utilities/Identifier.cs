using UnityEngine;
using Variables;

namespace Utilities
{
    public class Identifier : MonoBehaviour
    {
        [SerializeField] private ValueReference<string> id;

        public string ID => id.Value;
    }
}
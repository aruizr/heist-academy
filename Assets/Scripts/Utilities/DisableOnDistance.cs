using Codetox.Core;
using Codetox.Variables;
using UnityEngine;
using Variables;

namespace Utilities
{
    public class DisableOnDistance : MonoBehaviour
    {
        [SerializeField] private Vector3Variable playerPosition;
        [SerializeField] private GameObject toDisable;
        [SerializeField] private ValueReference<float> distance;

        private void Update()
        {
            if (toDisable.DistanceTo(playerPosition.Value) > distance.Value && toDisable.activeSelf)
                toDisable.SetActive(false);
            else if (toDisable.DistanceTo(playerPosition.Value) < distance.Value && !toDisable.activeSelf)
                toDisable.SetActive(true);
        }
    }
}
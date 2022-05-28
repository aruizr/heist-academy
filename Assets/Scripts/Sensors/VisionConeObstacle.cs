using Codetox.Messaging;
using UnityEngine;

namespace Sensors
{
    public class VisionConeObstacle : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.Send<Renderer>(r => r.enabled = true);
        }
    }
}
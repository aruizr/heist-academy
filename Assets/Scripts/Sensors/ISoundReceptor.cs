using UnityEngine;

namespace Sensors
{
    public interface ISoundReceptor
    {
        void ReceiveSound(GameObject source);
    }
}
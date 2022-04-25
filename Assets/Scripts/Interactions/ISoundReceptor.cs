using UnityEngine;

namespace Interactions
{
    public interface ISoundReceptor
    {
        void ReceiveSound(Vector3 sourcePoint);
    }
}
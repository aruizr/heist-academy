using UnityEngine;

namespace Interactions
{
    public interface IGrabbeable: ISelectible
    {
        void Grab(Transform parent);
        void Drop();
        void Throw(Vector3 velocity);
    }
}
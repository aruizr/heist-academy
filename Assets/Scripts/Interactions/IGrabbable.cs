using UnityEngine;

namespace Interactions
{
    public interface IGrabbable
    {
        void ToggleGrabDrop(Transform parent);
        bool IsGrabbed { get; }
    }
}
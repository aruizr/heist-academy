using System;
using UnityEngine;

namespace AI.Guard
{
    [Serializable]
    public struct RouteSpot
    {
        public Transform transform;
        public float time;
    }
}
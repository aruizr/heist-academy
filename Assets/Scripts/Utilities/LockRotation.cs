using System;
using UnityEngine;

namespace Utilities
{
    public class LockRotation : MonoBehaviour
    {
        [SerializeField] private bool x, y, z;
        
        private Vector3 _initial;

        private void Awake()
        {
            _initial = transform.eulerAngles;
        }

        private void Update()
        {
            var angles = transform.eulerAngles;

            if (x) angles.x = _initial.x;
            if (y) angles.y = _initial.y;
            if (z) angles.z = _initial.z;

            transform.eulerAngles = angles;
        }
    }
}
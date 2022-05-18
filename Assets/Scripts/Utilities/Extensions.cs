﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

namespace Utilities
{
    public static class Extensions
    {
        public static bool HasReachedDestination(this NavMeshAgent agent)
        {
            if (agent.pathPending) return false;
            if (!(agent.remainingDistance <= agent.stoppingDistance)) return false;
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }

        public static bool CanReachDestination(this NavMeshAgent agent)
        {
            return agent.pathStatus == NavMeshPathStatus.PathComplete;
        }

        public static Vector3 Rotate(this Vector3 vector, float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis) * vector;
        }

        public static IEnumerable<T> WrappedAround<T>(this IEnumerable<T> enumerable)
        {
            while (true)
                foreach (var item in enumerable)
                    yield return item;
        }

        public static void SetVolume(this AudioMixer mixer, string parameterName, float percentage)
        {
            if (percentage < 0 || percentage > 100) throw new ArgumentOutOfRangeException(nameof(percentage));
            mixer.SetFloat(parameterName, Mathf.Log10(percentage / 100) * 20);
        }

        public static bool GetVolume(this AudioMixer mixer, string parameterName, out float volume)
        {
            if (!mixer.GetFloat(parameterName, out volume)) return false;
            volume = 100 * Mathf.Pow(10, volume / 20);
            return true;
        }

        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }

        public static bool ToBool(this int value)
        {
            return value != 0;
        }
    }
}
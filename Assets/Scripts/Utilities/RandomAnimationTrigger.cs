using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class RandomAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private List<string> triggers;
        [SerializeField] private Range<float> timeRange;

        private void OnEnable()
        {
            StartCoroutine(DoRandomAnimation());
        }

        private void OnDisable()
        {
            StopCoroutine(DoRandomAnimation());
        }

        private IEnumerator DoRandomAnimation()
        {
            while (true)
            {
                var time = Random.Range(timeRange.min, timeRange.max);
                yield return new WaitForSeconds(time);
                var index = Random.Range(0, triggers.Count);
                var trigger = triggers[index];
                animator.SetTrigger(trigger);
            }
        }
    }
}
using System.Collections.Generic;
using Codetox.Core;
using Codetox.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Variables;

namespace Interactions
{
    public class Breakable : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private ValueReference<float> minVelocity;
        [SerializeField] private List<GameObject> shards;
        [SerializeField] private ValueReference<float> shardStartFadingDelay;
        [SerializeField] private ValueReference<float> shardFadingTime;

        public UnityEvent onBreak;
        public UnityEvent onNotBreak;

        public void Break()
        {
            if (rigidbody.velocity.magnitude < minVelocity.Value)
            {
                onNotBreak?.Invoke();
                return;
            }

            foreach (var shard in shards)
            {
                shard.SetActive(true);
                shard.transform.parent = null;
                shard.Send<Rigidbody>(rb =>
                {
                    rb.isKinematic = false;
                    rb.velocity = rigidbody.velocity;
                });
                shard.Send<Renderer>(r =>
                {
                    var material = r.material;
                    var currentColor = material.color;
                    var finalColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
                    
                    r.material.
                        DOColor(finalColor, shardFadingTime.Value).
                        SetDelay(shardStartFadingDelay.Value).
                        SetEase(Ease.Linear).
                        OnComplete(() => shard.gameObject.SetActive(false));
                });
            }
            
            onBreak?.Invoke();

            rigidbody.gameObject.SetActive(false);
        }
    }
}
using System.Collections.Generic;
using Codetox.Messaging;
using UnityEngine;
using Variables;

namespace Interactions
{
    public class Breakable : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private ValueReference<float> minVelocity;
        [SerializeField] private List<GameObject> shards;

        public void Break()
        {
            if (rigidbody.velocity.magnitude < minVelocity.Value) return;

            foreach (var shard in shards)
            {
                shard.SetActive(true);
                shard.transform.parent = null;
                shard.Send<Rigidbody>(rb =>
                {
                    rb.isKinematic = false;
                    rb.velocity = rigidbody.velocity;
                });
            }

            rigidbody.gameObject.SetActive(false);
        }
    }
}
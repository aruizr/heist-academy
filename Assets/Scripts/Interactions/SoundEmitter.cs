using System.Linq;
using Codetox.Core;
using Codetox.Messaging;
using UnityEngine;
using Variables;

namespace Interactions
{
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] private ValueReference<float> radius;
        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private Color gizmosColor = Color.white;

        public float Radius => radius.Value;

        public void Emit()
        {
            var position = transform.position;
            var colliders = Physics.OverlapSphere(position, Radius);

            foreach (var coll in colliders.Where(IsValid))
            {
                var point = coll.ClosestPoint(position);
                var ray = new Ray(position, transform.DirectionTo(point));

                if (Physics.Raycast(ray, transform.DistanceTo(point), obstacleLayers)) continue;

                coll.gameObject.Send<ISoundReceptor>(receptor => receptor.ReceiveSound(position),
                    MessageScope.Children);
            }
        }

        private static bool IsValid(Collider coll)
        {
            return coll is SphereCollider || coll is BoxCollider || coll is CapsuleCollider;
        }
    }
}
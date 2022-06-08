using System.Linq;
using Codetox.Core;
using Codetox.Messaging;
using DG.Tweening;
using UnityEngine;
using Variables;

namespace Sensors
{
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] private ValueReference<float> radius;
        [SerializeField] private ValueReference<LayerMask> obstacleLayers;
        [SerializeField] private Color gizmosColor = Color.white;

        public float Radius => radius.Value;

        public void Emit()
        {
            var position = transform.position;
            var colliders = Physics.OverlapSphere(position, Radius).Where(IsValid);

            foreach (var coll in colliders)
            {
                var obj = coll.gameObject;
                var point = coll.ClosestPoint(position);
                var ray = new Ray(position, transform.DirectionTo(point));

                if (Physics.Raycast(ray, transform.DistanceTo(point), obstacleLayers.Value)) continue;

                obj.Send<ISoundReceptor>(receptor => receptor.ReceiveSound(gameObject), MessageScope.Children);
            }
        }

        private static bool IsValid(Collider coll)
        {
            return coll is SphereCollider || coll is BoxCollider || coll is CapsuleCollider;
        }
    }
}
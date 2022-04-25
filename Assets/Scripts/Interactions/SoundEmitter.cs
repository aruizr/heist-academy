using Codetox.Core;
using Codetox.Messaging;
using UnityEngine;

namespace Interactions
{
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private Color gizmosColor = Color.white;

        public void Emit()
        {
            var position = transform.position;
            var colliders = Physics.OverlapSphere(position, radius);

            foreach (var coll in colliders)
            {
                var point = coll.ClosestPoint(position);
                var ray = new Ray(position, transform.DirectionTo(point));

                if (Physics.Raycast(ray, transform.DistanceTo(point), obstacleLayers)) continue;

                coll.gameObject.Send<ISoundReceptor>(receptor => receptor.ReceiveSound(position), MessageScope.Children);
            }
        }
    }
}
using UnityEngine;

namespace RuntimeSets
{
    [CreateAssetMenu(fileName = nameof(GameObjectRuntimeSet), menuName = "Trashy Games/Runtime Sets/GameObject")]
    public class GameObjectRuntimeSet: RuntimeSet<GameObject>
    {
    }
}
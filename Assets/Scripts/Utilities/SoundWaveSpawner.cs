using Codetox.Pooling;
using UnityEngine;

namespace Utilities
{
    public class SoundWaveSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        private Pool<GameObject> _pool;

        private void Awake()
        {
            _pool = new LifoPool<GameObject>(
                () =>
                {
                    var obj = Instantiate(prefab);
                    obj.SetActive(false);
                    return obj;
                },
                obj =>
                {
                    
                }
            );
        }

        public void SpawnWave(GameObject source)
        {
            
        }
    }
}
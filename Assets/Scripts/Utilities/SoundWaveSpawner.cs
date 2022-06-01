using System.Collections;
using Codetox.Messaging;
using Codetox.Pooling;
using DG.Tweening;
using Sensors;
using UnityEngine;

namespace Utilities
{
    public class SoundWaveSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] [Range(0, 1)] private float initialAlfa;
        [SerializeField] private int poolSize = 10;
        [SerializeField] private float expansionTime;
        [SerializeField] private Ease waveEase;

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
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                Destroy,
                poolSize
            );
        }

        public void SpawnWave(GameObject source)
        {
            source.Send<SoundEmitter>(emitter =>
            {
                var wave = _pool.Get();
                
                wave.transform.position = source.transform.position;
                wave.transform.up = Vector3.up;
                wave.transform.localScale = Vector3.one * 0.1f;
                wave.transform.
                    DOScale(Vector3.one * emitter.Radius * 2, expansionTime).
                    SetEase(waveEase).
                    OnComplete(() => _pool.Return(wave));
            
                wave.Send<SpriteRenderer>(spriteRenderer =>
                {
                    var color = spriteRenderer.color;

                    spriteRenderer.color = new Color(color.r, color.g, color.b, initialAlfa);
                    spriteRenderer.DOFade(0, expansionTime).SetEase(waveEase);
                }, MessageScope.Children);
            });
        }
    }
}
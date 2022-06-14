using Codetox.GameEvents;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(fileName = nameof(AudioClipGameEvent), menuName = "Trashy Games/Game Events/Audio Clip")]
    public class AudioClipGameEvent: GameEvent<AudioClip>
    {
    }
}
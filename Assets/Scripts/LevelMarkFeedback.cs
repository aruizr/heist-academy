using Codetox.Variables;
using UnityEngine;
using UnityEngine.Events;

public class LevelMarkFeedback : MonoBehaviour
{
    [SerializeField] private FloatVariable levelMark;

    public UnityEvent onLevelPassed;
    public UnityEvent onLevelFailed;

    public void DoFeedback()
    {
        (levelMark.Value < 5 ? onLevelFailed : onLevelPassed)?.Invoke();
    }
}
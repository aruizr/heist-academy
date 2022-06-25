using Codetox.GameEvents;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(fileName = nameof(ControlSchemeSwitchedGameEvent),
        menuName = "Trashy Games/Game Events/Control Scheme Switched")]
    public class ControlSchemeSwitchedGameEvent : GameEvent<ControlSchemeSwitchedGameEventData>
    {
    }
}
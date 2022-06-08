using MyBox;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = nameof(PlayerPrefsManager), menuName = "Trashy Games/PlayerPrefs Manager", order = 0)]
    public class PlayerPrefsManager : ScriptableObject
    {
        [ButtonMethod]
        private string DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            return "PlayerPrefs deleted.";
        }
    }
}
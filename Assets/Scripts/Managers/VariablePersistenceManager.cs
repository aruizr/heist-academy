using Codetox.Variables;
using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName = "Trashy Games/Persistence Manager", fileName = nameof(VariablePersistenceManager))]
    public class VariablePersistenceManager : ScriptableObject
    {
        private const string Key = "PersistentData";

        [SerializeField] private Variable<object>[] toPersist;

        private void OnEnable()
        {
            Load();
        }

        private void OnDisable()
        {
            Save();
        }

        public void Load()
        {
            if (!PlayerPrefs.HasKey(Key)) return;
            var json = PlayerPrefs.GetString(Key);
            toPersist = JsonUtility.FromJson<Variable<object>[]>(json);
        }

        public void Save()
        {
            var json = JsonUtility.ToJson(toPersist);
            PlayerPrefs.SetString(Key, json);
        }
    }
}
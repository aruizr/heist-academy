using System;
using System.Collections.Generic;
using System.Linq;
using Codetox.Messaging;
using Codetox.Variables;
using RuntimeSets;
using UnityEngine;
using Utilities;
using Variables;

[CreateAssetMenu(fileName = nameof(LevelEvaluation), menuName = "Trashy Games/Level Evaluation")]
public class LevelEvaluation : ScriptableObject
{
    [Serializable]
    public struct StatEvaluation
    {
        public IntVariable stat;
        public int bestValue;
        public int worstValue;
        [Range(0, 100)] public float percentage;
    }

    [Serializable]
    public struct CollectibleEvaluation
    {
        public ValueReference<string> collectibleID;
        [Range(0, 100)] public float percentage;
    }

    [SerializeField] private GameObjectRuntimeSet playerInventory;
    [SerializeField] private List<StatEvaluation> statEvaluations;
    [SerializeField] private List<CollectibleEvaluation> collectibleEvaluations;

    public float GetLevelMark()
    {
        var mark = 0f;

        foreach (var statEvaluation in statEvaluations)
        {
            var m = 10 / (statEvaluation.bestValue - statEvaluation.worstValue);
            var y = 10;
            var x = statEvaluation.bestValue;
            var b = y - m * x;
            var p = statEvaluation.percentage / 100;
            
            mark += (m * statEvaluation.stat.Value + b) * p;
        }
        
        foreach (var collectibleEvaluation in collectibleEvaluations)
        {
            foreach (var item in playerInventory)
            {
                item.Send<Identifier>(identifier =>
                {
                    if (identifier.ID.Equals(collectibleEvaluation.collectibleID.Value))
                        mark += 10 * (collectibleEvaluation.percentage / 100);
                });
            }
        }

        return Mathf.Round(mark * 100) / 100;
    }
}
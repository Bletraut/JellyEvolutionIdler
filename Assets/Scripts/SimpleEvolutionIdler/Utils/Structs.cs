using System.Collections;

using UnityEngine;

namespace SimpleEvolutionIdler.Utils
{

    [System.Serializable]
    public struct Value
    {
        public float Min;
        public float Max;
        public float Current;

        public Value(float min, float max) : this(min, max, 0) { }
        public Value(float min, float max, float current)
        {
            Min = min;
            Max = max;
            Current = current;
        }

        public void SetRandomCurrent()
        {
            Current = Random.Range(Min, Max);
        }
    }
}
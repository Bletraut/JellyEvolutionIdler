using System.Collections;
using UnityEngine;

namespace SimpleEvolutionIdler.World
{
    [CreateAssetMenu(fileName = "NewEnityType", menuName = @"Simple Evolution Idler/Entity Type", order = 0)]
    public class EntityType : ScriptableObject
    {
        [SerializeField]
        private string typeName;
        public string Name { get => typeName; }
    }
}
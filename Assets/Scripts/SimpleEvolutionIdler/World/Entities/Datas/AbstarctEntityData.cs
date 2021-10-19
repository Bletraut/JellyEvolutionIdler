using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SimpleEvolutionIdler.World
{
    public abstract class AbstaractEntityData : ScriptableObject
    {
        [SerializeField]
        private EntityType type;
        [SerializeField]
        private string entityName;

        public EntityType Type { get => type; }
        public string Name { get => entityName; }
        
        public abstract IEntity CreateEntity(Planet planet);
    }
}

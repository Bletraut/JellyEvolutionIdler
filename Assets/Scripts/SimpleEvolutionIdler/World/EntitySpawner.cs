using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.Core;

namespace SimpleEvolutionIdler.World
{
    public class EntitySpawner : Timer
    {
        [SerializeField]
        private Planet planet;
        public Planet Planet { get => planet; }

        [SerializeField]
        private AbstaractEntityData _entityData;
        public AbstaractEntityData EntityData { get => _entityData; }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            this.OnComplete.AddListener(SpawnEntity);
        }

        public void SpawnEntity()
        {
            planet.TryAddEntity(_entityData);
        }
    }
}
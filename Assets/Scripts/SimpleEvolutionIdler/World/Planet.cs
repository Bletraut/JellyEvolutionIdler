using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

namespace SimpleEvolutionIdler.World
{
    public class Planet: MonoBehaviour
    {
        [SerializeField]
        private string _planetName = "";
        public string PlanetName { get => _planetName; }

        [SerializeField]
        private List<EntitySettings> _entitiesSettings = new List<EntitySettings>();

        private List<IEntity> _entities = new List<IEntity>();
        public ReadOnlyCollection<IEntity> Entities;


        // Start is called before the first frame update
        public virtual void Start()
        {
            Entities = new ReadOnlyCollection<IEntity>(_entities);
        }

        public virtual int GetFreePlaces(AbstaractEntityData entityData)
        {
            EntitySettings entitySettings = _entitiesSettings.FirstOrDefault(t => t.Type == entityData.Type);
            if (entitySettings == null)
            {
                return 0;
            }

            var entitiesCount = _entities.Count(e => e.EntityData.Type == entityData.Type);
            return entitySettings.MaxCount - entitiesCount;
        }

        public virtual bool TryAddEntity(AbstaractEntityData entityData)
        {
            return TryAddEntity(entityData, out _);
        }

        public virtual bool TryAddEntity(AbstaractEntityData entityData, params IEntity[] displacedEntites)
        {
            return TryAddEntity(entityData, out _, displacedEntites);
        }

        public virtual bool TryAddEntity(AbstaractEntityData entityData, out IEntity entity, params IEntity[] displacedEntites)
        {
            entity = null;

            EntitySettings entitySettings = _entitiesSettings.FirstOrDefault(t => t.Type == entityData.Type);
            if (entitySettings == null)
            {
                return false;
            }

            var entitiesCount = _entities.Except(displacedEntites).Count(e => e.EntityData.Type == entityData.Type);
            if (entitiesCount >= entitySettings.MaxCount)
            {
                return false;
            }

            entity = CreateEntityFromData(entityData);
            _entities.Add(entity);

            displacedEntites.ToList().ForEach(e => FreeEntity(e));

            return true;
        }

        public void FreeEntity(IEntity entity)
        {
            _entities.Remove(entity);
            entity.Free();
        }

        protected virtual IEntity CreateEntityFromData(AbstaractEntityData entityData)
        {
            return entityData.CreateEntity(this);
        }
    }

    [Serializable]
    public class EntitySettings
    {
        public EntityType Type;
        public int MaxCount;
    }
}

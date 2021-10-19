using System.Linq;
using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.Controllers;

namespace SimpleEvolutionIdler.World
{
    [CreateAssetMenu(fileName = "NewVisualEnityData", menuName = @"Simple Evolution Idler/Visual Entity Data", order = 100)]
    public class VisualEntityData : AbstaractEntityData
    {
        [SerializeField]
        private GameObject _gameObjectPrefab;
        public GameObject GameObjectPrefab { get => _gameObjectPrefab; }

        public override IEntity CreateEntity(Planet planet)
        {
            return CreateEntity<VisualEntity>(planet);
        }
        public virtual IEntity CreateEntity<TEntity>(Planet planet) where TEntity : VisualEntity
        {
            var gameObject = Instantiate(GameObjectPrefab, planet.transform);
            TEntity visualEntity = gameObject.GetComponent<TEntity>() ?? gameObject.AddComponent<TEntity>();
            visualEntity.Build(this, planet);

            return visualEntity;
        }
    }
}
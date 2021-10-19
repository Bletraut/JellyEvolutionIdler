using System.Collections;
using UnityEngine;

namespace SimpleEvolutionIdler.World
{
    [CreateAssetMenu(fileName = "NewContainerEnityData", menuName = @"Simple Evolution Idler/Container Entity Data", order = 101)]
    public class ContainerEntityData : VisualEntityData
    {
        [SerializeField]
        private AbstaractEntityData _containedEntityData;
        public AbstaractEntityData ContainedEntityData { get => _containedEntityData; }
    }
}
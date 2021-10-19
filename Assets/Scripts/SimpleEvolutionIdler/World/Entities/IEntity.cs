using System.Collections;

namespace SimpleEvolutionIdler.World
{
    public interface IEntity
    {
        AbstaractEntityData EntityData { get; }
        Planet Planet { get; }

        void Build(AbstaractEntityData entityData, Planet planet);
        void Free();
    }
}
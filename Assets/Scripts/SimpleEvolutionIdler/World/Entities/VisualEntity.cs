using System.Collections;
using UnityEngine;

namespace SimpleEvolutionIdler.World
{
    public class VisualEntity : MonoBehaviour, IEntity
    {
        public const float OverlayZIndex = -9f;

        private AbstaractEntityData _entityData;
        public AbstaractEntityData EntityData { get => _entityData; }
        private Planet _planet;
        public Planet Planet { get => _planet; }

        public float ZIndex
        {
            get => transform.position.z;
            protected set
            {
                var pos = transform.position;
                pos.z = value;
                transform.position = pos;
            }
        }
        public bool CanUpdateZIndex { get; set; } = true;

        public virtual void Build(AbstaractEntityData entityData, Planet planet)
        {
            _entityData = entityData;
            _planet = planet;
        }

        public virtual void Free()
        {
            Destroy(gameObject);
        }

        void Update()
        {
            UpdateZIndex();
        }

        public void UpdateZIndex()
        {
            if (!CanUpdateZIndex) return;
            ZIndex = gameObject.transform.position.y;
        }

        public void SetZIndexToOverlay()
        {
            ZIndex = OverlayZIndex;
        }
    }
}
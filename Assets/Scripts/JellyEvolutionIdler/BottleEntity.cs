using System.Collections;

using UnityEngine;

using DG.Tweening;

using SimpleEvolutionIdler.World;
using SimpleEvolutionIdler.World.Controls;

namespace JellyEvolutionIdler
{
    public class BottleEntity : BoundaryPlanetEntity
    {
        private ContainerEntityData _containerEntityData;
        public new ContainerEntityData EntityData 
        {
            get
            {
                if (_containerEntityData == null) _containerEntityData = base.EntityData as ContainerEntityData;
                return _containerEntityData;
            }
        }

        private AbstarctPicker picker;

        protected override void Start()
        {
            base.Start();

            picker = gameObject.AddComponent<SimplePicker>();
            picker.CanPick = false;
            picker.PointerUp += Picker_PointerUp;

            var targetPos = Planet.GetRandomValidPosition();
            gameObject.transform.position = new Vector3(targetPos.x, 15f, 0);
            this.CanUpdateZIndex = false;
            this.ZIndex = targetPos.y;

            gameObject.transform.DOMoveY(targetPos.y, 1f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => 
            {
                picker.CanPick = true;
            });
        }

        public void Open()
        {
            if (Planet.TryAddEntity(EntityData.ContainedEntityData, out IEntity entity, this))
            {
                var visualEntity = entity as VisualEntity;
                if (visualEntity != null)
                {
                    visualEntity.gameObject.transform.position = gameObject.transform.position;
                }
            }
        }

        private void Picker_PointerUp()
        {
            Open();
        }

        public override void Free()
        {
            picker.PointerUp -= Picker_PointerUp;

            base.Free();
        }
    }
}
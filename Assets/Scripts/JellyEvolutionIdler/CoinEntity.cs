using System.Collections;

using UnityEngine;

using DG.Tweening;

using SimpleEvolutionIdler.World;
using SimpleEvolutionIdler.World.Controls;

namespace JellyEvolutionIdler
{
    public class CoinEntity : BoundaryPlanetEntity
    {
        private CoinEntityData _coinEntityData;
        public new CoinEntityData EntityData
        {
            get
            {
                if (_coinEntityData == null) _coinEntityData = base.EntityData as CoinEntityData;
                return _coinEntityData;
            }
        }

        public int Multipler { get; set; } = 1;

        private AbstarctPicker picker;

        protected override void Start()
        {
            base.Start();

            picker = gameObject.AddComponent<SimplePicker>();
            picker.PointerUp += Picker_PointerUp; ;

            gameObject.transform.DOPunchScale
            (
                punch: new Vector3(-0.25f, 0.25f, 0),
                duration: 1,
                vibrato: 5
            );
        }

        private void Picker_PointerUp()
        {
            picker.CanPick = false;

            this.CanUpdateZIndex = false;
            this.SetZIndexToOverlay();

            var leftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

            gameObject.transform.DOKill();
            gameObject.transform.localScale = Vector3.one;

            var moveSeq = DOTween.Sequence();
            moveSeq.Append(gameObject.transform.DOShakeScale(0.15f));
            moveSeq.Append(gameObject.transform.DOMove(leftCorner, 0.35f).SetEase(Ease.InOutCubic));
            moveSeq.OnComplete(() =>
            {
                Planet.AddCoins(EntityData.BaseProfit * Multipler);
                Planet.FreeEntity(this);
            });
        }

        public override void Free()
        {
            picker.PointerUp -= Picker_PointerUp;

            base.Free();
        }
    }
}
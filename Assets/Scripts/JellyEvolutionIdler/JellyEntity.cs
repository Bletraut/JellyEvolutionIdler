using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

using SimpleEvolutionIdler.World;
using SimpleEvolutionIdler.World.Controls;

namespace JellyEvolutionIdler
{
    public class JellyEntity : BoundaryPlanetEntity, IMergeable
    {
        private JellyEntityData _jellyEntityData;
        public new JellyEntityData EntityData
        {
            get
            {
                if (_jellyEntityData == null) _jellyEntityData = base.EntityData as JellyEntityData;
                return _jellyEntityData;
            }
        }

        private IMerger _merger;
        public IMerger Merger { get => _merger; }

        private MergeStatuses _mergeStatus = MergeStatuses.Available;
        public MergeStatuses MergeStatus
        {
            get => _mergeStatus; 
            private set
            {
                if (_mergeStatus != value)
                {
                    _mergeStatus = value;
                    MergeStatusChanged?.Invoke(MergeStatus);
                }
            }
        }

        public event System.Action<MergeStatuses> MergeStatusChanged;
        public event System.Action MergeCancelled;

        private AbstarctDragger dragger;
        private AbstarctPicker picker;

        private RandomWalker walker;

        private Tweener squeeze;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _merger = new VisualMerger(this.Planet);

            dragger = gameObject.AddComponent<SimpleDragger>();
            picker = gameObject.AddComponent<SimplePicker>();
            picker.CanSolidPick = false;

            dragger.DragStarted += Dragger_DragStarted;
            dragger.DragEnded += Dragger_DragEnded;
            picker.PointerUp += Picker_PointerUp;

            Merger.MergeRequested += Merger_MergeRequested;
            Merger.MergeCancelled += Merger_Failed;
            Merger.MergeFailed += Merger_Failed;

            walker = gameObject.AddComponent<RandomWalker>();

            gameObject.transform.DOPunchScale
            (
                punch: new Vector3(-0.25f, 0.25f, 0),
                duration: 1,
                vibrato: 5
            )
            .OnComplete(() =>
            {
                walker.Play();
            });
        }

        public void SpawnCoin()
        {
            if (Planet.GetFreePlaces(EntityData.CoinData) == 0) return;

            AddCoin();

            squeeze?.Kill();
            gameObject.transform.localScale = Vector3.one;
            squeeze = gameObject.transform.DOPunchScale
            (
                punch: new Vector3(0.25f, -0.25f, 0),
                duration: 1,
                vibrato: 5
            );
        }
        private void AddCoin()
        {
            if (Planet.TryAddEntity(EntityData.CoinData, out IEntity entity))
            {
                var coinEntity = entity as CoinEntity;
                if (coinEntity != null)
                {
                    coinEntity.Multipler = EntityData.Level;
                    coinEntity.gameObject.transform.position = gameObject.transform.position + new Vector3(-0.85f, -0.1f, 0);
                }
            }
        }

        private void Merger_MergeRequested(IMergeable mergeable)
        {
            _mergeStatus = MergeStatuses.Preparing;
            dragger.CanDrag = false;
            walker.Stop();

            var otherPos = (mergeable as VisualEntity).gameObject.transform.position;
            var middle = gameObject.transform.position + (otherPos - gameObject.transform.position) / 2;

            var sequence = DOTween.Sequence();
            sequence.Append(gameObject.transform.DOMove(middle, 0.1f));
            sequence.AppendCallback(() => 
            {
                MergeStatus = MergeStatuses.Ready;
                gameObject.transform.DOKill();
            });
        }
        private void Merger_Failed()
        {
            MergeStatus = MergeStatuses.Available;
            dragger.CanDrag = true;
            walker.Play();
        }

        private void Picker_PointerUp()
        {
            SpawnCoin();
        }

        private void Dragger_DragStarted()
        {
            walker.Stop();
        }
        private void Dragger_DragEnded()
        {
            Merger.Merge(this);
        }

        public override void Free()
        {
            walker.Stop();

            dragger.DragEnded -= Dragger_DragStarted;
            dragger.DragEnded -= Dragger_DragEnded;
            picker.PointerUp -= Picker_PointerUp;

            Merger.MergeRequested -= Merger_MergeRequested;
            Merger.MergeCancelled -= Merger_Failed;
            Merger.MergeFailed -= Merger_Failed;

            base.Free();
        }
    }
}

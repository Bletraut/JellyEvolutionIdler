using System.Collections;

using UnityEngine;

using DG.Tweening;

using SimpleEvolutionIdler.Utils;

namespace JellyEvolutionIdler
{
    [RequireComponent(typeof(BoundaryPlanetEntity))]
    public class RandomWalker : MonoBehaviour
    {
        public Value Duration = new Value(1f, 3f);

        private BoundaryPlanetEntity entity;
        private Sequence moveSeq;

        void Start()
        {
            entity = GetComponent<BoundaryPlanetEntity>();
        }

        public void Play()
        {
            Stop();

            var targetPos = entity.Planet.GetRandomValidPosition();
            Duration.SetRandomCurrent();

            moveSeq = DOTween.Sequence();
            moveSeq.AppendInterval(Duration.Current / 2);
            moveSeq.Append(gameObject.transform.DOMoveX(targetPos.x, Duration.Current * 2).SetEase(Ease.InOutQuart));
            moveSeq.Join(gameObject.transform.DOMoveY(targetPos.y, Duration.Current * 2).SetEase(Ease.InOutQuart));
            moveSeq.OnComplete(() => Play());
        }

        public void Stop()
        {
            moveSeq?.Kill();
        }
    }
}
using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.World;

namespace JellyEvolutionIdler
{
    [CreateAssetMenu(fileName = "NewCoinData", menuName = @"Jelly Evolution Idler/Coin Data", order = 102)]
    public class CoinEntityData : VisualEntityData
    {
        [SerializeField]
        private int _baseProfit;
        public int BaseProfit { get => _baseProfit; }

        public override IEntity CreateEntity(Planet planet)
        {
            return base.CreateEntity<CoinEntity>(planet);
        }
    }
}
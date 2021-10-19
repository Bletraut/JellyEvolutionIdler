using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.World;

namespace JellyEvolutionIdler
{
    [CreateAssetMenu(fileName = "NewJellyCharacterData", menuName = @"Jelly Evolution Idler/Jelly Character Data", order = 101)]
    public class JellyEntityData : ContainerEntityData
    {
        [SerializeField]
        private int _level;
        public int Level { get => _level; }

        [SerializeField]
        private CoinEntityData  _coinData;
        public CoinEntityData CoinData { get => _coinData; }

        public override IEntity CreateEntity(Planet planet)
        {
            return base.CreateEntity<JellyEntity>(planet);
        }
    }
}
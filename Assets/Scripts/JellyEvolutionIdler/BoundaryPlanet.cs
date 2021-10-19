using System.Linq;
using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.World;

namespace JellyEvolutionIdler
{
    public class BoundaryPlanet : Planet
    {
        public BoxCollider2D Boundary;

        public int Coins { get; private set; } = 0;

        public override void Start()
        {
            base.Start();
        }

        public void AddCoins(int count)
        {
            Coins += count;
        }

        public Vector3 GetRandomValidPosition()
        {
            var bounds = Boundary.bounds;

            Vector3 pos;
            do
            {
                pos = new Vector3()
                {
                    x = Random.Range(bounds.min.x, bounds.max.x),
                    y = Random.Range(bounds.min.y, bounds.max.y),
                    z = 0
                };
            }
            while (!CheckPositionValidation(pos));


            return pos;
        }

        public bool CheckPositionValidation(Vector3 pos)
        {
           return Physics2D.OverlapPointAll(pos).Any(n => n == Boundary);
        }
    }
}
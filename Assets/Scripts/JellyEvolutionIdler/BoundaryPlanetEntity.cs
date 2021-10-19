using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.World;

namespace JellyEvolutionIdler
{
    public class BoundaryPlanetEntity : VisualEntity
    {
        private BoundaryPlanet _jellyPlanet;
        public new BoundaryPlanet Planet 
        { 
            get
            {
                if (_jellyPlanet == null) _jellyPlanet = base.Planet as BoundaryPlanet;
                return _jellyPlanet;
            } 
        }

        protected virtual void Start()
        {
            if (Planet == null)
            {
                Debug.LogError($"JellyEntity can only live on a JellyPlanet. But you have {base.Planet.GetType()}.");
            }
        }
    }
}
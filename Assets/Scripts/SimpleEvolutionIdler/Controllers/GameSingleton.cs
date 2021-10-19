using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

using SimpleEvolutionIdler.Core;
using SimpleEvolutionIdler.World;

namespace SimpleEvolutionIdler.Controllers
{
    public class GameSingleton : Singleton<GameSingleton>
    {
        public enum GameState
        {
            Loading,
            Start,
            Play,
            Pause
        }
        public GameState State { get; private set; } = GameState.Play;

        public InputController InputController { get; private set; }

        public Planet CurrentPlanet { get; private set; }

        private List<Planet> _planets;
        public ReadOnlyCollection<Planet> Planets;

        public List<AbstaractEntityData> Entities = new List<AbstaractEntityData>();

        // Start is called before the first frame update
        void Start()
        {
            InputController = Object.FindObjectOfType<InputController>();

            _planets = Object.FindObjectsOfType<Planet>().ToList();
            Planets = new ReadOnlyCollection<Planet>(_planets);

            if (_planets.Count > 0)
            {
                SetCurrentPlanet(0);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCurrentPlanet(int index)
        {
            CurrentPlanet = _planets[index];
        }
        public void SetCurrentPlanet(Planet planet)
        {
            var index = _planets.IndexOf(planet);
            if (index < 0)
            {
                AddPlanet(planet);
                index = _planets.Count - 1;
            }
            SetCurrentPlanet(index);
        }
        public bool TryAddPlanet(Planet planet)
        {
            if (_planets.Contains(planet)) return false;

            AddPlanet(planet);
            return true;
        }
        private void AddPlanet(Planet planet)
        {
            _planets.Add(planet);
        }
    }
}

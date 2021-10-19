using System.Collections;

using UnityEngine;
using TMPro;

namespace JellyEvolutionIdler
{
    public class TopUI : MonoBehaviour
    {
        public TMP_Text CoinsText;
        public BoundaryPlanet Planet;

        // Update is called once per frame
        void Update()
        {
            CoinsText.text = $"{Planet.Coins}";
        }
    }
}
using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.Controllers;

namespace SimpleEvolutionIdler.World.Controls
{
    public abstract class AbstractInput : MonoBehaviour
    {
        public bool IsInputBlocked { get; protected set; } = false;

        void Awake()
        {
            InputController.Instance?.RegisterInput(this);
        }
        void OnDestroy()
        {
            InputController.Instance?.UnregisterInput(this);
        }
    }
}
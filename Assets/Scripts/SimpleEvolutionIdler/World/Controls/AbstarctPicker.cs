using System;
using System.Collections;

using UnityEngine;

namespace SimpleEvolutionIdler.World.Controls
{
    public abstract class AbstarctPicker : AbstractInput
    {
        public bool CanPick { get; set; } = true;
        public bool CanSolidPick { get; set; } = true;

        public virtual event Action PointerDown;
        public virtual event Action PointerUp;

        protected virtual void OnPonterDown()
        {
            PointerDown?.Invoke();
        }
        protected virtual void OnPointerUp()
        {
            PointerUp?.Invoke();
        }
    }
}
using System;
using System.Collections;

using UnityEngine;

namespace SimpleEvolutionIdler.World.Controls
{
    public abstract class AbstarctDragger : AbstractInput
    {
        public bool CanDrag { get; set; } = true;
        public bool IsDrag { get; protected set; } = false;

        public virtual event Action DragStarted;
        public virtual event Action Drag;
        public virtual event Action DragEnded;

        protected virtual void OnDragStarted()
        {
            DragStarted?.Invoke();
        }
        protected virtual void OnDrag()
        {
            Drag?.Invoke();
        }
        protected virtual void OnDragEnded()
        {
            DragEnded?.Invoke();
        }
    }
}
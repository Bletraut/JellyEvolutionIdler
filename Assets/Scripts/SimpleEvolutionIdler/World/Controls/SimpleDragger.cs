using System;
using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.Controllers;

namespace SimpleEvolutionIdler.World.Controls
{
    [RequireComponent(typeof(Collider2D), typeof(VisualEntity))]
    public class SimpleDragger : AbstarctDragger
    {
        protected Vector3 offset = Vector3.zero;

        private VisualEntity visualEntity;

        void Start()
        {
            visualEntity = GetComponent<VisualEntity>();
        }

        void OnMouseDown()
        {
            if (!CanDrag) return;

            IsDrag = true;
            IsInputBlocked = true;

            visualEntity.CanUpdateZIndex = false;
            visualEntity.SetZIndexToOverlay();
            offset = gameObject.transform.position - GameSingleton.Instance.InputController.PointerPosition;

            OnDragStarted();
        }
        void OnMouseDrag()
        {
            if (!IsDrag) return;

            gameObject.transform.position = offset + GameSingleton.Instance.InputController.PointerPosition;

            OnDrag();
        }
        void OnMouseUp()
        {
            IsDrag = false;
            IsInputBlocked = false;

            visualEntity.CanUpdateZIndex = true;
            visualEntity.UpdateZIndex();

            OnDragEnded();
        }
    }
}
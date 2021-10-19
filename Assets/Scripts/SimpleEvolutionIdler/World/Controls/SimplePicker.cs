using System.Collections;

using UnityEngine;

using SimpleEvolutionIdler.Controllers;

namespace SimpleEvolutionIdler.World.Controls
{
    [RequireComponent(typeof(Collider2D), typeof(VisualEntity))]
    public class SimplePicker : AbstarctPicker
    {
        void OnMouseDown()
        {
            if (!CanPick) return;
            OnPonterDown();
        }
        void OnMouseUp()
        {
            if (!CanPick) return;
            OnPointerUp();
        }

        void OnMouseEnter()
        {
            if (CanSolidPick && InputController.Instance.InputType == InputController.InputTypes.Solid)
            {
                OnMouseUp();
            }
        }
    }
}
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using SimpleEvolutionIdler.Core;
using SimpleEvolutionIdler.World.Controls;

namespace SimpleEvolutionIdler.Controllers
{
    public class InputController : Singleton<InputController>
    {
        public TrailRenderer SolidEffect;

        public enum InputTypes
        {
            Single,
            Solid,
        }
        public InputTypes InputType { get; private set; } = InputTypes.Single;
        public float MinSolidDistance { get; private set; } = 0.2f;

        public Vector3 PointerPosition
        {
            get
            {
                var pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pointerPos.z = 0;

                return pointerPos;
            }
        }

        private List<AbstractInput> inputs = new List<AbstractInput>();
        private Vector3 startPos = Vector3.zero;
        private bool isSolidAvailable = false;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSolidAvailable = !inputs.Any(n => n.IsInputBlocked);
                if (isSolidAvailable)
                {
                    startPos = PointerPosition;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isSolidAvailable = false;
                InputType = InputTypes.Single;
                SolidEffect.Clear();
                SolidEffect.gameObject.SetActive(false);
            }

            if (isSolidAvailable && !SolidEffect.gameObject.activeSelf)
            {
                var isSolid = (startPos - PointerPosition).magnitude > MinSolidDistance;
                if (isSolid)
                {
                    SolidEffect.gameObject.SetActive(true);
                    InputType = InputTypes.Solid;
                }
            }

            if (SolidEffect.gameObject.activeSelf)
            {
                SolidEffect.transform.position = PointerPosition;
            }
        }

        public void RegisterInput(AbstractInput input)
        {
            inputs.Add(input);
        }
        public void UnregisterInput(AbstractInput input)
        {
            inputs.Remove(input);
        }
    }
}
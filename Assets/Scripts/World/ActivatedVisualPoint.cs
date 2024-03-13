using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace World
    {
        public class ActivatedVisualPoint : WayPoint
        {
            [SerializeField] private GameObject hunt;

            private void Start()
            {
                hunt.SetActive(IsTarget);
            }

            protected override void OnPassed()
            {
                hunt.SetActive(false);
            }

            protected override void OnAssignAsTarget()
            {
                hunt.SetActive(true);
            }

        }

    }
}


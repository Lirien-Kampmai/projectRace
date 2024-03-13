using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace RS
    {
        public class RaceKeyStarter : MonoBehaviour, IDependency<StateTracker>
        {
            private StateTracker stateTracker;
            public void CreateDependency(StateTracker obj) => stateTracker = obj;

            private void Update()
            {
                KeyStarter();
            }
            
            private void KeyStarter()
            {
                if (Input.GetKeyDown(KeyCode.Return) == true)
                {
                    stateTracker.LaunchPreparationStart();
                }
            }
        }
    }
}



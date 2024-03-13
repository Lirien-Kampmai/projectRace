using ProjectCar.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace RS
    {
        public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<StateTracker>
        {
            private CarInputControl m_CarInputControl;
            private StateTracker    stateTracker;
            public void CreateDependency(StateTracker obj)    => stateTracker      = obj;
            public void CreateDependency(CarInputControl obj) => m_CarInputControl = obj;

            private void Start()
            {
                stateTracker.m_Started   += OnRaceStarted;
                stateTracker.m_Complited += OnRaceFinished;

                m_CarInputControl.enabled = false;
            }

            private void OnDestroy()
            {
                stateTracker.m_Started   -= OnRaceStarted;
                stateTracker.m_Complited -= OnRaceFinished;
            }

            private void OnRaceStarted()
            {
                m_CarInputControl.enabled = true;
            }

            private void OnRaceFinished()
            {
                m_CarInputControl.enabled = false;
                m_CarInputControl.Stop();
            }
        }
    }

}

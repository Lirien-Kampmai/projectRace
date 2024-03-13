using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace RS
    {
        public class RaceTimeTracker : MonoBehaviour, IDependency<StateTracker>
        {
            private StateTracker stateTracker;
            public void CreateDependency(StateTracker obj) => stateTracker = obj;

            private float currentTime;
            public float CurrentTime => currentTime;

            private void Start()
            {
                stateTracker.m_Started   += OnRaceStarted;
                stateTracker.m_Complited += OnRaceComplited;

                enabled = false;
            }

            private void OnDestroy()
            {
                stateTracker.m_Started   += OnRaceStarted;
                stateTracker.m_Complited += OnRaceComplited;
            }

            private void OnRaceStarted()
            {
                enabled = true;
                currentTime = 0;
            }
            private void OnRaceComplited()
            {
                enabled = false;
            }

            private void Update()
            {
                currentTime += Time.deltaTime;
            }
        }
    }
}



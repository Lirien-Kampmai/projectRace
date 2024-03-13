using ProjectCar.RS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCar
{
    namespace UI
    {
        public class UITimeTracker : MonoBehaviour, IDependency<StateTracker>, IDependency<RaceTimeTracker>
        {
            [SerializeField] private Text text;

            private StateTracker    stateTracker;
            private RaceTimeTracker raceTimeTracker;

            public void CreateDependency(RaceTimeTracker obj) => raceTimeTracker = obj;
            public void CreateDependency(StateTracker obj)    => stateTracker    = obj;

            private void Start()
            {
                stateTracker.m_Started   += OnRaceStarted;
                stateTracker.m_Complited += OnRaceComplited;

                text.enabled = false;
            }
            private void OnDestroy()
            {
                stateTracker.m_Started   -= OnRaceStarted;
                stateTracker.m_Complited -= OnRaceComplited;
            }
            private void OnRaceStarted()
            {
                text.enabled = true;
                enabled      = true;
            }
            private void OnRaceComplited()
            {
                text.enabled = false;
                enabled      = false;
            }

            private void Update()
            {
                text.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
            }
        }
    }
}



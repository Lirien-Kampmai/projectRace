using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCar
{
    namespace RS
    {
        public class UICountDownTimer : MonoBehaviour, IDependency<StateTracker>, IDependency<Timer>
        {
            private StateTracker stateTracker;
            private Timer        timer;

            [SerializeField] private Text text;
            
            public void CreateDependency(StateTracker obj) => stateTracker = obj;
            public void CreateDependency(Timer obj)        => timer        = obj;

            private void Start()
            {
                stateTracker.m_PreparationStart += OnRacePeparationStarted;
                stateTracker.m_Started          += OnStarted;

                //text.enabled = false;
            }

            private void OnDestroy()
            {
                stateTracker.m_PreparationStart -= OnRacePeparationStarted;
                stateTracker.m_Started          -= OnStarted;
            }

            private void Update()
            {
                text.text = timer.Value.ToString("F0");

                if (text.text == "0") text.text = "GO";
                if (text.text == "4" || timer.enabled == false) text.text = "Press Enter to Go";
            }

            private void OnStarted()
            {
                text.enabled = false;
                enabled      = false;
            }

            private void OnRacePeparationStarted()
            {
                text.enabled = true;
                enabled      = true;
            }
        }
    }
}



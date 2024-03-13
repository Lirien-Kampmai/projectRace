using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ProjectCar.RS;
using UnityEngine;

namespace ProjectCar
{
    namespace Cam
    {
        public class CameraControl : MonoBehaviour, IDependency<StateTracker>
        {

            private StateTracker stateTracker;

            public void CreateDependency(StateTracker obj) => stateTracker = obj;

            [SerializeField] private CinemachineFreeLook freeLoookCamera;

            private void Start()
            {
                stateTracker.m_PreparationStart += OnRacePeparationStarted;
                stateTracker.m_Started += OnStarted;
                stateTracker.m_Complited += OnComplited;
            }

            private void OnDestroy()
            {
                stateTracker.m_PreparationStart -= OnRacePeparationStarted;
                stateTracker.m_Started -= OnStarted;
                stateTracker.m_Complited += OnComplited;
            }

            private void OnRacePeparationStarted()
            {
                freeLoookCamera.Priority = 9;
            }

            private void OnStarted()
            {
                freeLoookCamera.Priority = 10;
            }

            private void OnComplited()
            {
                freeLoookCamera.Priority = 1;
            }


        }
    }
}



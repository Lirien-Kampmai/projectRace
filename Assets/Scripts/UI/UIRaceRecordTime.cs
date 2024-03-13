using ProjectCar.RS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCar
{
    namespace UI
    {
        public class UIRaceRecordTime : MonoBehaviour, IDependency<StateTracker>, IDependency<RaceResultTime>
        {
            private StateTracker   stateTracker;
            private RaceResultTime resultTime;

            public void CreateDependency(StateTracker obj)   => stateTracker = obj;
            public void CreateDependency(RaceResultTime obj) => resultTime   = obj;

            [SerializeField] private GameObject goldRecordObj;
            [SerializeField] private GameObject playerRecordObj;
            [SerializeField] private Text goldRecordTime;
            [SerializeField] private Text playerRecordTime;

            private void Start()
            {
                stateTracker.m_Started   += OnRaceStarted;
                stateTracker.m_Complited += OnRaceComplited;

                goldRecordObj.  SetActive(false);
                playerRecordObj.SetActive(false);
            }
            private void OnDestroy()
            {
                stateTracker.m_Started   -= OnRaceStarted;
                stateTracker.m_Complited -= OnRaceComplited;
            }
            private void OnRaceStarted()
            {
                if(resultTime.RecordTime > resultTime.GoldTime || resultTime.RecordTime == 0)
                {
                    goldRecordObj.SetActive(true);
                    goldRecordTime.text = StringTime.SecondToTimeString(resultTime.GoldTime);
                }

                if(resultTime.RecordTime != 0)
                {
                    playerRecordObj.SetActive(true);
                    playerRecordTime.text = StringTime.SecondToTimeString(resultTime.RecordTime);
                }

            }
            private void OnRaceComplited()
            {
                goldRecordObj.  SetActive(false);
                playerRecordObj.SetActive(false);
            }
        }
    }
}



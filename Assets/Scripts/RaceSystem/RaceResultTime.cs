using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectCar
{
    namespace RS
    {
        public class RaceResultTime : MonoBehaviour, IDependency<StateTracker>, IDependency<RaceTimeTracker>
        {
            public const string SaveMark = "_player_best_time";

            [SerializeField] private float m_GoldTime;
            private float m_RecordTime;
            private float m_CurrentTime;

            public float GoldTime => m_GoldTime;
            public float RecordTime => m_RecordTime;
            public float CurrentTime => m_CurrentTime;

            public event UnityAction ResultUpdate;

            private RaceTimeTracker raceTimeTracker;
            private StateTracker    stateTracker;
            public void CreateDependency(StateTracker obj)    => stateTracker    = obj;
            public void CreateDependency(RaceTimeTracker obj) => raceTimeTracker = obj;

            private void Awake()
            {
                Load();
            }

            private void Start()
            {
                stateTracker.m_Complited += OnRaceComplited;
            }

            private void OnDestroy()
            {
                stateTracker.m_Complited -= OnRaceComplited;
            }

            private void OnRaceComplited()
            {
                float absolutRecord = GetAbsolutRecord();
                if (raceTimeTracker.CurrentTime < absolutRecord || m_RecordTime == 0)
                {
                    m_RecordTime = raceTimeTracker.CurrentTime;
                    Save();
                }
                m_CurrentTime = raceTimeTracker.CurrentTime;
                ResultUpdate?.Invoke();
            }

            public float GetAbsolutRecord()
            {
                if (m_RecordTime < m_GoldTime && m_RecordTime != 0)
                    return m_RecordTime;
                else
                    return m_GoldTime;
            }

            private void Load()
            {
                m_RecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
            }

            private void Save ()
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, m_RecordTime);
            }
        }
    }
}



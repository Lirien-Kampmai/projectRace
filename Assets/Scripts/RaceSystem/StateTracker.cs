using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ProjectCar.World;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectCar
{
    namespace RS
    {
        public enum RaceState
        {
            Preparation,
            TimerToStart,
            Race,
            ComplitedRace
        }

        public class StateTracker : MonoBehaviour, IDependency<WayCircle>, IDependency<Timer>
        {
            public event UnityAction      m_PreparationStart;
            public event UnityAction      m_Started;
            public event UnityAction      m_Complited;
            public event UnityAction<int> m_LapComplited;

            public event UnityAction<WayPoint> m_TrackPointPassed;

            private WayCircle m_TrackPointCircle;
            private Timer     m_CountDownTimer;

            [SerializeField] private int m_LapToComplite;

            public void CreateDependency(WayCircle obj) => m_TrackPointCircle = obj;
            public void CreateDependency(Timer obj)     => m_CountDownTimer   = obj;

            private RaceState state;
            public RaceState State => state;

            private void StartState(RaceState state)
            {
                this.state = state;
            }

            private void Start()
            {
                m_CountDownTimer.enabled   = false;
                m_CountDownTimer.Finished += OnCountDownTimerFinished;

                StartState(RaceState.Preparation);

                m_TrackPointCircle.OnWayPointTriggered += OnWayPointTriggered;
                m_TrackPointCircle.LapComplit          += OnLapComplit;
            }

            private void OnCountDownTimerFinished()
            {
                StartRace();
            }

            private void OnDestroy()
            {
                m_TrackPointCircle.OnWayPointTriggered -= OnWayPointTriggered;
                m_TrackPointCircle.LapComplit          -= OnLapComplit;
                m_CountDownTimer.Finished              -= OnCountDownTimerFinished;
            }

            private void OnWayPointTriggered(WayPoint wayPoint)
            {
                m_TrackPointPassed?.Invoke(wayPoint);
            }

            private void OnLapComplit(int lapAmount)
            {
                if(m_TrackPointCircle.TrackType == TrackType.Sprint)
                {
                    CompliteRace();
                }

                if (m_TrackPointCircle.TrackType == TrackType.Circular)
                {
                    if (lapAmount == m_LapToComplite)
                        CompliteRace();
                    else
                        CompliteLap(lapAmount);
                }
            }

            public void LaunchPreparationStart()
            {
                if (state != RaceState.Preparation) return;
                StartState(RaceState.TimerToStart);

                m_CountDownTimer.enabled = true;

                m_PreparationStart?.Invoke();
            }

            private void StartRace()
            {
                if (state != RaceState.TimerToStart) return;
                StartState(RaceState.Race);
                m_Started?.Invoke();
            }

            private void CompliteRace()
            {
                if (state != RaceState.Race) return;
                StartState(RaceState.ComplitedRace);

                m_Complited?.Invoke();
            }

            private void CompliteLap(int lapAmount)
            {
                m_LapComplited?.Invoke(lapAmount);
            }
        }
    }
}

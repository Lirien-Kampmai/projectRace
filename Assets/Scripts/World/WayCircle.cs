using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectCar
{
    namespace World
    {
        public enum TrackType
        {
            Circular,
            Sprint
        }

        public class WayCircle : MonoBehaviour
        {
            [SerializeField] private TrackType m_TrackType;

            public event UnityAction<int> LapComplit;
            public event UnityAction<WayPoint> OnWayPointTriggered;

            public TrackType TrackType => m_TrackType;
            private WayPoint[] m_Points;

            private int LapsComplited = -1;

            private void Awake()
            {
                BuildCircle();
            }

            private void Start()
            {
                for (int i = 0; i < m_Points.Length; i++)
                {
                    m_Points[i].OnWayPoint += OnTrackPointTriggered;
                }

                m_Points[0].AssignAsTarget();
            }

            [ContextMenu(nameof(BuildCircle))]
            private void BuildCircle()
            {
                m_Points = WayPointBuilder.Build(transform, m_TrackType);
            }

            private void OnDestroy()
            {
                for (int i = 0; i < m_Points.Length; i++)
                {
                    m_Points[i].OnWayPoint -= OnTrackPointTriggered;
                }
            }
            private void OnTrackPointTriggered(WayPoint wayPoint)
            {
                if (wayPoint.IsTarget == false) return;

                wayPoint.Passed();
                wayPoint.Next?.AssignAsTarget();

                OnWayPointTriggered?.Invoke(wayPoint); 

                if (wayPoint.IsLast == true)
                {
                    LapsComplited++;

                    if(m_TrackType == TrackType.Sprint)
                        LapComplit?.Invoke(LapsComplited);

                    if (m_TrackType == TrackType.Circular)
                        if (LapsComplited > 0)
                            LapComplit?.Invoke(LapsComplited);
                }
            }
        }
    }
}



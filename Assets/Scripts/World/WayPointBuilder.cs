using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectCar
{
    namespace World
    {
        public static class WayPointBuilder
        {
            public static WayPoint[] Build(Transform trackTransform, TrackType trackType)
            {
                WayPoint[] m_Points = new WayPoint[trackTransform.childCount];

                ResetPoint(trackTransform, m_Points);
                MakeLinks (m_Points, trackType);
                MarkPoint (m_Points, trackType);

                return m_Points;
            }

            private static void ResetPoint(Transform trackTransform, WayPoint[] m_Points)
            {
                for (int i = 0; i < m_Points.Length; i++)
                {
                    m_Points[i] = trackTransform.GetChild(i).GetComponent<WayPoint>();

                    if (m_Points[i] == null)
                    {
                        Debug.LogError("WayPoint[] = null");
                        return;
                    }
                    m_Points[i].ResetWaypointBool();
                }
            }

            private static void MakeLinks(WayPoint[] m_Points, TrackType trackType)
            {
                for (int i = 0; i < m_Points.Length - 1; i++)
                {
                    m_Points[i].Next = m_Points[i + 1];
                }

                if (trackType == TrackType.Circular)
                    m_Points[m_Points.Length - 1].Next = m_Points[0];
            }

            private static void MarkPoint(WayPoint[] m_Points, TrackType trackType)
            {
                m_Points[0].IsFirst = true;

                if (trackType == TrackType.Sprint)
                    m_Points[m_Points.Length - 1].IsLast = true;

                if (trackType == TrackType.Circular)
                    m_Points[0].IsLast = true;
            }
        }
    }
}



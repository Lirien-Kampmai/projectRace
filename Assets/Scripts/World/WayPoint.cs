using ProjectCar.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectCar
{
    namespace World
    {
        public class WayPoint : MonoBehaviour
        {
            public event UnityAction<WayPoint> OnWayPoint;

            protected virtual void OnPassed() { }
            protected virtual void OnAssignAsTarget() { }

            public WayPoint Next;

            public bool IsFirst;
            public bool IsLast;

            protected bool isTarget;
            public bool IsTarget => isTarget;

            private void OnTriggerEnter(Collider other)
            {
                if (other.transform.root.GetComponent<CarInfoModel>() == null) return;

                OnWayPoint?.Invoke(this);
            }

            public void Passed()
            {
                isTarget = false;
                OnPassed();
            }

            public void AssignAsTarget() 
            {
                isTarget = true;
                OnAssignAsTarget();
            }

            public void ResetWaypointBool()
            {
                Next    = null;
                IsFirst = false;
                IsLast  = false;
            }
        }
    }
}



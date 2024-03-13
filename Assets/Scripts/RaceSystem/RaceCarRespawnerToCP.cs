using ProjectCar.Car;
using ProjectCar.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace RS
    {
        public class RaceCarRespawnerToCP : MonoBehaviour, IDependency<StateTracker>, IDependency<CarInfoModel>, IDependency<CarInputControl>
        {
            [SerializeField] private float respawnerHeight;

            private WayPoint        respawnWayPoint;
            private StateTracker    stateTracker;
            private CarInfoModel    infoModel;
            private CarInputControl inputControl;

            public void CreateDependency(StateTracker obj)    => stateTracker = obj;
            public void CreateDependency(CarInfoModel obj)    => infoModel    = obj;
            public void CreateDependency(CarInputControl obj) => inputControl = obj;

            private void Start()
            {
                stateTracker.m_TrackPointPassed += OnTrackPointPassed;
            }

            private void OnDestroy()
            {
                stateTracker.m_TrackPointPassed -= OnTrackPointPassed;
            }

            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.R) == true)
                    Respawn();
            }

            private void OnTrackPointPassed(WayPoint point)
            {
                respawnWayPoint = point;
            }

            public void Respawn()
            {
                if (respawnWayPoint == null) return;

                if (stateTracker.State != RaceState.Race) return;

                infoModel.Respawn(respawnWayPoint.transform.position + respawnWayPoint.transform.up * respawnerHeight, respawnWayPoint.transform.rotation);

                inputControl.Stop();
            }
        }
    }
}



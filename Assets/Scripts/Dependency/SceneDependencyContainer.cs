using ProjectCar.Car;
using ProjectCar.RS;
using ProjectCar.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneDependencyContainer : Dependency
{
    [SerializeField] private WayCircle       wayCircle;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private StateTracker    stateTracker;
    [SerializeField] private CarInfoModel    carInfoModel;
    [SerializeField] private Timer           timer;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime  raceResultTime;

    protected override void LinkAll(MonoBehaviour monoBehaviourInScene)
    {
        Link<WayCircle>      (wayCircle,       monoBehaviourInScene);
        Link<CarInputControl>(carInputControl, monoBehaviourInScene);
        Link<StateTracker>   (stateTracker,    monoBehaviourInScene);
        Link<CarInfoModel>   (carInfoModel,    monoBehaviourInScene);
        Link<Timer>          (timer,           monoBehaviourInScene);
        Link<RaceTimeTracker>(raceTimeTracker, monoBehaviourInScene);
        Link<RaceResultTime> (raceResultTime,  monoBehaviourInScene);
    }

    private void Awake()
    {
        FindAllObjToBind();
    }

}

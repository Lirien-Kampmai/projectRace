using ProjectCar.RS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
{
    private RaceResultTime raceResultTime;
    public void CreateDependency(RaceResultTime obj) => raceResultTime = obj;

    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Text recordTime;
    [SerializeField] private Text currentTime;

    private void Start()
    {
        resultPanel.SetActive(false);

        raceResultTime.ResultUpdate += OnResultUpdate;
    }

    private void OnDestroy()
    {
        raceResultTime.ResultUpdate -= OnResultUpdate;
    }

    private void OnResultUpdate()
    {
        resultPanel.SetActive(true);

        recordTime.text = StringTime.SecondToTimeString(raceResultTime.GetAbsolutRecord());
        currentTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);




    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
{
    private Pauser pauser;
    public void CreateDependency(Pauser obj) => pauser = obj;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject timeCurrent;
    [SerializeField] private GameObject timeGold;

    private void Start()
    {
        panel.SetActive(false);
        pauser.PauseStateChange += OnPauser_PauseStateChange;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauser_PauseStateChange;
    }

    private void OnPauser_PauseStateChange(bool isPause)
    {
        panel.SetActive(isPause);
        timeCurrent.SetActive(!isPause);
        timeGold.SetActive(!isPause);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
            pauser.ChangePauseState();
    }

    public void UnPause()
    {
        pauser.UnPause();
    }
}

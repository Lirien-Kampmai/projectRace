using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioSourse : MonoBehaviour, IDependency<Pauser>
{
    private Pauser pauser;
    public void CreateDependency(Pauser obj) => pauser = obj;

    private new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        pauser.PauseStateChange += OnPauseStateChange;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChange;
    }

    private void OnPauseStateChange(bool pause)
    {
        if (pause == false) audio.Play();
        if (pause == true ) audio.Stop();
    }
}

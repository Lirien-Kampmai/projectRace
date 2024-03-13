using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependencyContainer : Dependency
{
    [SerializeField] private Pauser pauser;

    private static GlobalDependencyContainer instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; 

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoad;

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
    
    protected override void LinkAll(MonoBehaviour monoBehaviourInScene)
    {
        Link<Pauser>(pauser, monoBehaviourInScene);
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjToBind();
    }
}

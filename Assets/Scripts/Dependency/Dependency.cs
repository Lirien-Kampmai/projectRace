using ProjectCar.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected virtual void LinkAll(MonoBehaviour monoBehaviourInScene)
    {
         
    }

    protected void FindAllObjToBind()
    {
        MonoBehaviour[] monoScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoScene.Length; i++)
        {
            LinkAll(monoScene[i]);
        }
    }
    protected void Link<T>(MonoBehaviour bindObj, MonoBehaviour target) where T : class
    {
        if (target is IDependency<T>) (target as IDependency<T>).CreateDependency(bindObj as T);
    }



    


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace UI
    {
        public class UIRaceSpawnButton : MonoBehaviour
        {
            [SerializeField] private Transform parent;
            [SerializeField] private UIRaceButton prefab;
            [SerializeField] private RaceInfo[] properties;

            [ContextMenu(nameof(Spawn))]
            public void Spawn()
            {
                if (Application.isPlaying == true) return;

                DestroyChildObj();

                for (int i = 0; i < properties.Length; i++)
                {
                    UIRaceButton button = Instantiate(prefab, parent);
                    button.ApplyProperty(properties[i]);
                }
            }

            private void DestroyChildObj()
            {
                GameObject[] allObj = new GameObject[parent.childCount];

                for (int i = 0; i < parent.childCount; i++)
                {
                    allObj[i] = parent.GetChild(i).gameObject;
                }

                for (int i = 0; i < allObj.Length; i++)
                {
                    DestroyImmediate(allObj[i]);
                }
            }
        }
    }
}



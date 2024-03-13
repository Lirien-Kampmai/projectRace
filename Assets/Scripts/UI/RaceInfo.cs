using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace UI
    {
        [CreateAssetMenu]
        public class RaceInfo : ScriptableObject
        {
            [SerializeField] private string sceneName;
            public string SceneName => sceneName;
            [SerializeField] private string title;
            public string Title => title;
            [SerializeField] private Sprite sprite;
            public Sprite Sprite => sprite;

        }
    }
}


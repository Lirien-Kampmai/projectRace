using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectCar
{
    namespace RS
    {
        public class Timer : MonoBehaviour
        {
            public event UnityAction Finished;

            [SerializeField] private float m_time;

            private float value;
            public float Value => value;

            private void Start()
            {
                value = m_time;
            }

            private void Update()
            {
                value -= Time.deltaTime;

                if(value <= 0)
                {
                    enabled = false;

                    Finished?.Invoke();
                }
            }
        }
    }
}

using ProjectCar.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCar
{
    namespace UI
    {
        public class SpeedIndicator : MonoBehaviour, IDependency<CarInfoModel>
        {
            private CarInfoModel m_Car;

            public void CreateDependency(CarInfoModel obj) => m_Car = obj;

            [SerializeField] private Text m_Text;

            private void Update()
            {
                SpeedText();
            }

            private void SpeedText()
            {
                m_Text.text = m_Car.m_linearVelocity.ToString("F0");
            }

        }
    }
}


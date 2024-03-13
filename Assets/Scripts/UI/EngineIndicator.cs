using ProjectCar.Car;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCar
{ 
    namespace UI
    {
        [System.Serializable]
        class EngineIndicatorColor
        {
            public float MaxRPM;
            public Color engineColor;
        }

        public class EngineIndicator : MonoBehaviour, IDependency<CarInfoModel>
        {
            private CarInfoModel m_Car;

            [SerializeField] private Image m_ImageRPM;
            [SerializeField] private Text  m_TextGear;

            [SerializeField] private EngineIndicatorColor[] m_EngineIndicatorColor;

            public void CreateDependency(CarInfoModel obj) => m_Car = obj;

            private void Update()
            {
                FillAmountImage();
                ColorBar();
                GearText();
            }

            private void FillAmountImage()
            {
                float engine = m_Car.EngineRPM / m_Car.EngineMaxRPM;
                m_ImageRPM.fillAmount = engine;
            }

            private void ColorBar()
            {
                for (int i = 0; i < m_EngineIndicatorColor.Length; i++)
                {
                    if (m_Car.EngineRPM <= m_EngineIndicatorColor[i].MaxRPM)
                    {
                        m_ImageRPM.color = m_EngineIndicatorColor[i].engineColor;
                        break;
                    }
                }
            }

            private void GearText()
            {
                m_TextGear.text = m_Car.SelectedGearIndex.ToString("F0");
            }
        }
    }
}


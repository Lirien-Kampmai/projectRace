using ProjectCar.RS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace Car
    {
        public class CarInputControl : MonoBehaviour, IDependency<CarInfoModel>
        {
            #region Variables
            [SerializeField] private AnimationCurve m_BrakeCurve;
            [SerializeField] private AnimationCurve m_SteerCurve;
            [SerializeField][Range(0.0f, 1.0f)] private float m_AutoBrakeStrenght;

            public void CreateDependency(CarInfoModel obj) => m_CarInfoModel = obj;

            private CarInfoModel m_CarInfoModel;
            private float m_wheelSpeed;
            private float m_verticalAxis;
            private float m_horizontalAxis;
            //rivate float m_handBrakeAxis;
            #endregion

            private void Update()
            {
                m_wheelSpeed = m_CarInfoModel.WheelSpeed;

                UpdateThrottle();
                //UpdateBrake();
                UpdateSteer();
                UpdateAutoBrake();
                UpdateAxis();
            }

            private void UpdateSteer()
            {
                m_CarInfoModel.m_SteerControll = m_SteerCurve.Evaluate(m_CarInfoModel.WheelSpeed / m_CarInfoModel.MaxSpeed) * m_horizontalAxis;
            }

            private void UpdateThrottle()
            {
                if (Mathf.Sign(m_verticalAxis) == Mathf.Sign(m_wheelSpeed) || Mathf.Abs(m_wheelSpeed) < 0.5f)
                {
                    m_CarInfoModel.m_ThrottleControll = Mathf.Abs(m_verticalAxis);
                    m_CarInfoModel.m_BrakeControll    = 0;
                }
                else
                {
                    m_CarInfoModel.m_ThrottleControll = 0;
                    m_CarInfoModel.m_BrakeControll    = m_BrakeCurve.Evaluate(m_wheelSpeed / m_CarInfoModel.MaxSpeed);
                }

                if (m_verticalAxis < 0 && m_wheelSpeed > -0.5f && m_wheelSpeed <= 0.5f) m_CarInfoModel.ShiftToReverseGear();
                if (m_verticalAxis > 0 && m_wheelSpeed > -0.5f && m_wheelSpeed < 0.5f)  m_CarInfoModel.ShiftToFirstGear();
            }

            private void UpdateAxis()
            {
                m_verticalAxis   = Input.GetAxis("Vertical");
                m_horizontalAxis = Input.GetAxis("Horizontal");
                //m_handBrakeAxis = Input.GetAxis("Jump");
            }

            private void UpdateAutoBrake()
            {
                if(m_verticalAxis == 0)
                {
                    m_CarInfoModel.m_BrakeControll = m_BrakeCurve.Evaluate(m_wheelSpeed / m_CarInfoModel.MaxSpeed);
                }
            }

            private void Reset()
            {
                m_verticalAxis   = 0;
                m_horizontalAxis = 0;

                m_CarInfoModel.m_BrakeControll    = 0;
                m_CarInfoModel.m_SteerControll    = 0;
                m_CarInfoModel.m_ThrottleControll = 0;
            }

            public void Stop()
            {
                Reset();
                m_CarInfoModel.m_BrakeControll = 1;
            }
        }
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace Car
    {
        [RequireComponent(typeof(Rigidbody))]
        public class CarPhisics : MonoBehaviour
        {
            #region Variables
            [Header("Wheel Axle")]
            [SerializeField] private WheelPhisics[] m_WheelAxle;

            [Header("Wheel Axle (pos.Z front + pos.Z back wheel)")]
            [SerializeField] private float m_WheelBaseLength;

            [Header("Center mass")]
            [SerializeField] private Transform m_CenterOfMass;

            [Header("Down Force")]
            [SerializeField] private float m_DownForceMin;
            [SerializeField] private float m_DownForceMax;
            [SerializeField] private float m_DownForceFactor;

            [Header ("Angular Drag")]
            [SerializeField] private float m_AngularDragMin;
            [SerializeField] private float m_AngularDragMax;
            [SerializeField] private float m_AngularDragFactor;

            [Header("Debug")]
            public float m_MotorTorque;
            public float m_BrakeTorque;
            public float m_SteerAngle;

            public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;
            private new Rigidbody rigidbody;
            #endregion

            private void Start()
            {
                rigidbody = GetComponent<Rigidbody>();

                if (m_CenterOfMass != null)
                    rigidbody.centerOfMass = m_CenterOfMass.localPosition;

                for(int i = 0; i < m_WheelAxle.Length; ++i)
                    m_WheelAxle[i].ConfVenicleSubstep(50, 50, 50);
            }

            private void FixedUpdate()
            {
                UpdateAngularDrag();
                UpdateDownForce();
                UpdateWhellAxle();
            }

            public float GetAvaregeRPMAxle()
            {
                float sum = 0;

                for (int i = 0; i < m_WheelAxle.Length; i++)
                {
                    sum += m_WheelAxle[i].GetAvaregeRPMWheel();
                }

                return sum / m_WheelAxle.Length;
            }

            public float GetWheelSpeed()
            {
                return GetAvaregeRPMAxle() * m_WheelAxle[0].GetRadiusWheelCollider() * 2 * 0.1885f;
            }

            private void UpdateAngularDrag()
            {
                rigidbody.angularDrag = Mathf.Clamp(m_AngularDragFactor * LinearVelocity, m_AngularDragMin, m_AngularDragMax);
            }

            private void UpdateDownForce()
            {
                float downForce = Mathf.Clamp(m_DownForceFactor * LinearVelocity, m_DownForceMin, m_DownForceMax);
                rigidbody.AddForce(-transform.up * downForce);
            }

            private void UpdateWhellAxle()
            {
                int amountMotorWheel = 0;

                for (int i = 0; i < m_WheelAxle.Length; i++)
                {
                    if (m_WheelAxle[i].IsMotor == true)
                        amountMotorWheel += 2;
                }

                for (int i = 0; i < m_WheelAxle.Length; i++)
                {
                    m_WheelAxle[i].Update();

                    m_WheelAxle[i].ApplyMotorTorque(m_MotorTorque / amountMotorWheel);
                    m_WheelAxle[i].ApplySteerAngle (m_SteerAngle, m_WheelBaseLength);
                    m_WheelAxle[i].ApplyBreakTorque(m_BrakeTorque);
                }
            }

            public void Reset()
            {
                rigidbody.velocity =        Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}





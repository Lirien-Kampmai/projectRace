using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace ProjectCar
{
    namespace Car
    {
        // class describes the physics of the wheel axle
        [System.Serializable]
        public class WheelPhisics
        {
            #region Variables
            [Header("Collider and Wheel")]
            // link to collider
            [SerializeField] private WheelCollider m_LeftWheelCollider;
            [SerializeField] private WheelCollider m_RightWheelCollider;
            // link to transform
            [SerializeField] private Transform m_LeftWheelMesh;
            [SerializeField] private Transform m_RightWheelMesh;

            [Header("Motor and Steer")]
            // condition wheels - turning or driving
            [SerializeField] private bool m_IsMotor;
            [SerializeField] private bool m_IsSteer;

            [Header("Force")]
            [SerializeField] private float m_AddDownForceWheel;
            [SerializeField] private float m_AntiRollForce;

            [Header("Stiffnes")]
            [SerializeField] private float m_BaseForwardStiffnes    = 1.5f;
            [SerializeField] private float m_StabilityForwardFactor = 1.0f;
            [SerializeField] private float m_BaseSidewayStiffnes    = 2.0f;
            [SerializeField] private float m_StabilitySidewayFactor = 1.0f;

            [Header("Wheel Width (posX L-wheel + posX R-wheel)")]
            [SerializeField] private float m_WheelWidth; // pos.X 1-wheel

            private WheelHit m_LeftWheelHit;
            private WheelHit m_RightWheelHit;

            public bool IsMotor => m_IsMotor;
            public bool IsSteer => m_IsSteer;
            #endregion

            public void Update()
            {
                UpdateWheelHit();
                ApplyAntyRoll();
                ApplyDownForce();
                CorrectStiffness();
                SynchCollTrans();
            }

            public void ConfVenicleSubstep(float speedThreshold, int speedBelowThreshold, int speedAboveThreshold)
            {
                m_LeftWheelCollider. ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, speedAboveThreshold);
                m_RightWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, speedAboveThreshold);
            }

            public float GetAvaregeRPMWheel()
            {
                return (m_LeftWheelCollider.rpm + m_RightWheelCollider.rpm) * 0.5f;
            }

            public float GetRadiusWheelCollider()
            {
                return m_LeftWheelCollider.radius;
            }

            // turn the wheel
            public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
            {
                if (m_IsSteer == false) return;

                #region Ackermann angle
                float radius    = Mathf.Abs(wheelBaseLength * Mathf.Tan( Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle) ) ) );
                float angleSing = Mathf.Sign(steerAngle);

                if(steerAngle > 0)
                {
                    m_LeftWheelCollider.steerAngle  = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_WheelWidth * 0.5f) ) ) * angleSing;
                    m_RightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_WheelWidth * 0.5f)))   * angleSing;
                }
                else if (steerAngle < 0)
                {
                    m_LeftWheelCollider.steerAngle  = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_WheelWidth * 0.5f))) * angleSing;
                    m_RightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_WheelWidth * 0.5f))) * angleSing;
                }
                else
                {
                    m_LeftWheelCollider.steerAngle  = 0;
                    m_RightWheelCollider.steerAngle = 0;
                }
                #endregion
            }

            // move the wheel forward
            public void ApplyMotorTorque(float motorTorque)
            {
                if (m_IsMotor == false) return;

                m_LeftWheelCollider.motorTorque  = motorTorque;
                m_RightWheelCollider.motorTorque = motorTorque;
            }

            // stopping the wheel
            public void ApplyBreakTorque(float brakeTorque)
            {
                m_LeftWheelCollider.brakeTorque  = brakeTorque;
                m_RightWheelCollider.brakeTorque = brakeTorque;
            }

            private void UpdateWheelHit()
            {
                m_LeftWheelCollider .GetGroundHit(out m_LeftWheelHit);
                m_RightWheelCollider.GetGroundHit(out m_RightWheelHit);
            }

            private void CorrectStiffness()
            {
                WheelFrictionCurve leftForward  = m_LeftWheelCollider.forwardFriction;
                WheelFrictionCurve rightForward = m_RightWheelCollider.forwardFriction;

                WheelFrictionCurve leftSideway  = m_LeftWheelCollider.sidewaysFriction;
                WheelFrictionCurve rightSideway = m_RightWheelCollider.sidewaysFriction;

                leftForward.stiffness  = m_BaseForwardStiffnes + Mathf.Abs(m_LeftWheelHit.forwardSlip)  * m_StabilityForwardFactor;
                rightForward.stiffness = m_BaseForwardStiffnes + Mathf.Abs(m_RightWheelHit.forwardSlip) * m_StabilityForwardFactor;

                leftSideway.stiffness = m_BaseSidewayStiffnes + Mathf.Abs(m_LeftWheelHit.sidewaysSlip)  * m_StabilitySidewayFactor;
                leftSideway.stiffness = m_BaseSidewayStiffnes + Mathf.Abs(m_RightWheelHit.sidewaysSlip) * m_StabilitySidewayFactor;

                m_LeftWheelCollider.forwardFriction  = leftForward;
                m_RightWheelCollider.forwardFriction = rightForward;

                m_LeftWheelCollider.sidewaysFriction  = leftSideway;
                m_RightWheelCollider.sidewaysFriction = rightSideway;
            }

            private void ApplyDownForce()
            {
                if (m_LeftWheelCollider.isGrounded == true)
                    m_LeftWheelCollider.attachedRigidbody.AddForceAtPosition(
                        m_LeftWheelHit.normal * -m_AddDownForceWheel * m_LeftWheelCollider.attachedRigidbody.velocity.magnitude, m_LeftWheelCollider.transform.position);

                if (m_RightWheelCollider.isGrounded == true)
                    m_RightWheelCollider.attachedRigidbody.AddForceAtPosition(
                        m_RightWheelHit.normal * -m_AddDownForceWheel * m_RightWheelCollider.attachedRigidbody.velocity.magnitude, m_RightWheelCollider.transform.position);
            }

            private void ApplyAntyRoll()
            {
                float travelLeft  = 1.0f;
                float travelRight = 1.0f;

                if(m_LeftWheelCollider.isGrounded == true)
                    travelLeft = (-m_LeftWheelCollider.transform.InverseTransformPoint(m_LeftWheelHit.point).y - m_LeftWheelCollider.radius) / m_LeftWheelCollider.suspensionDistance;

                if (m_LeftWheelCollider.isGrounded == true)
                    travelRight = (-m_RightWheelCollider.transform.InverseTransformPoint(m_RightWheelHit.point).y - m_RightWheelCollider.radius) / m_RightWheelCollider.suspensionDistance;

                float forceDir = (travelLeft - travelRight);

                if (m_LeftWheelCollider.isGrounded == true)
                    m_LeftWheelCollider.attachedRigidbody.AddForceAtPosition(m_LeftWheelCollider.transform.up * -forceDir * m_AntiRollForce, m_LeftWheelCollider.transform.position);

                if (m_RightWheelCollider.isGrounded == true)
                    m_RightWheelCollider.attachedRigidbody.AddForceAtPosition(m_RightWheelCollider.transform.up * forceDir * m_AntiRollForce, m_RightWheelCollider.transform.position);

            }

            // synchronization of collider and transform
            private void SynchCollTrans()
            {
                UpdateWheelTransform(m_LeftWheelCollider,  m_LeftWheelMesh);
                UpdateWheelTransform(m_RightWheelCollider, m_RightWheelMesh);
            }

            // updates wheel position and rotation using transform and collider
            private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
            {
                Vector3 position;
                Quaternion rotation;

                wheelCollider.GetWorldPose(out position, out rotation);
                wheelTransform.position = position;
                wheelTransform.rotation = rotation;
            }
        }
    }
}

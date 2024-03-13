using UnityEngine;

namespace ProjectCar
{
    namespace Car
    {
        [RequireComponent(typeof(CarPhisics))]
        public class CarInfoModel : MonoBehaviour
        {
            #region Variables
            [Header("Stats")]
            [SerializeField] private float m_MaxBrakeTorque;
            [SerializeField] private float m_MaxSteerAngle;
            [SerializeField] private float m_MaxSpeed;
            
            [Header("Stats Debug")]
            public float m_ThrottleControll;
            public float m_BrakeControll;
            public float m_SteerControll;
            //public float HandBrakeControll;
            public float m_linearVelocity;

            [Header("Engine")]
            [SerializeField] private AnimationCurve m_EngineTorqueCurve;
            [SerializeField] private float m_EngineMaxTorque;
            [SerializeField] private float m_EngineMinRPM;
            [SerializeField] private float m_EngineMaxRPM;
            [SerializeField] private float m_UpShiftEngineRPM;
            [SerializeField] private float m_DownShiftEngineRPM;
            
            [Header("Engine Debug")]
            [SerializeField] private float m_EngineTorque;
            [SerializeField] private float m_EngineRPM;

            [Header("Gearbox")]
            [SerializeField] private float[] m_Gear;
            [SerializeField] private float m_FinalDriveRatio;
            
            [Header("Gearbox Debug")]
            [SerializeField] private float m_SelectedGear;
            [SerializeField] private float m_RearGear;
            [SerializeField] private int m_SelectedGearIndex;

            public float LinearVelocity          => m_CarPhisics.LinearVelocity;
            public float NormalizeLinearVelocity => m_CarPhisics.LinearVelocity / MaxSpeed;
            public float WheelSpeed              => m_CarPhisics.GetWheelSpeed();
            public float MaxSpeed                => m_MaxSpeed;
            public float EngineRPM               => m_EngineRPM;
            public float EngineMaxRPM            => m_EngineMaxRPM;
            public int SelectedGearIndex         => m_SelectedGearIndex;

            private CarPhisics m_CarPhisics;

            #endregion

            private void Start()
            {
                m_CarPhisics = GetComponent<CarPhisics>();
            }

            private void Update()
            {
                m_linearVelocity = LinearVelocity;

                UpdateEngineTorque();

                AutoGearShift();

                if (LinearVelocity >= m_MaxSpeed)
                {
                    m_EngineTorque = 0;
                }

                m_CarPhisics.m_MotorTorque = m_EngineTorque * m_ThrottleControll;
                m_CarPhisics.m_BrakeTorque = m_MaxBrakeTorque * m_BrakeControll;
                m_CarPhisics.m_SteerAngle = m_MaxSteerAngle * m_SteerControll;
            }

            #region Gear
            private void ShiftGear(int gearIndex)
            {
                gearIndex = Mathf.Clamp(gearIndex, 0, m_Gear.Length - 1);
                m_SelectedGear = m_Gear[gearIndex];
                m_SelectedGearIndex = gearIndex;
            }

            private void AutoGearShift()
            {
                if (m_SelectedGear < 0) return;

                if (m_EngineRPM >= m_UpShiftEngineRPM) UpGear();

                if (m_EngineRPM < m_DownShiftEngineRPM) DownGear();
            }

            public void UpGear()
            {
                ShiftGear(m_SelectedGearIndex + 1);
            }

            public void DownGear()
            {
                ShiftGear(m_SelectedGearIndex - 1);
            }

            public void ShiftToReverseGear()
            {
                m_SelectedGear = m_RearGear;
            }

            public void ShiftToFirstGear()
            {
                ShiftGear(0);
            }

            public void ShiftToNetral()
            {
                m_SelectedGear = 0;
            }



            #endregion

            private void UpdateEngineTorque()
            {
                m_EngineRPM = m_EngineMinRPM + Mathf.Abs(m_CarPhisics.GetAvaregeRPMAxle() * m_SelectedGear * m_FinalDriveRatio);

                m_EngineRPM = Mathf.Clamp(m_EngineRPM, m_EngineMinRPM, m_EngineMaxRPM);

                m_EngineTorque = m_EngineTorqueCurve.Evaluate(m_EngineRPM / m_EngineMaxRPM) * m_EngineMaxTorque * m_FinalDriveRatio * Mathf.Sign(m_SelectedGear) * m_Gear[0];



            }

            public void ResetCar()
            {
                m_CarPhisics.Reset();

                m_CarPhisics.m_MotorTorque = 0;
                m_CarPhisics.m_BrakeTorque = 0;
                m_CarPhisics.m_SteerAngle  = 0;

                m_ThrottleControll = 0;
                m_BrakeControll    = 0;
                m_SteerControll    = 0;
        }

            public void Respawn(Vector3 pos, Quaternion rot)
            {
                ResetCar();

                transform.position = pos;
                transform.rotation = rot;
            }
        }
    }
}



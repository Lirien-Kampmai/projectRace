using ProjectCar.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace SFX
    {
        public class WheelEfect : MonoBehaviour, IDependency<CarInfoModel>
        {
            [SerializeField] private WheelCollider [] m_WheelColliders;
            [SerializeField] private ParticleSystem[] m_Smoke;
            [SerializeField] private AudioSource m_Audio;
            [SerializeField] private GameObject  m_SkidPrefab;

            [SerializeField] private float m_ForwardSlipLimit;
            [SerializeField] private float m_SidewaySlipLimit;

            public void CreateDependency(CarInfoModel obj) => carInfoModel = obj;

            private WheelHit wheelHit;
            private Transform[] m_SkidTrail;
            private CarInfoModel carInfoModel;

            private void Start()
            {
                m_WheelColliders = carInfoModel.GetComponentsInChildren<WheelCollider>();
                m_SkidTrail = new Transform[m_WheelColliders.Length];
                m_Smoke = GetComponentsInChildren<ParticleSystem>();
            }

            private void Update()
            {
                bool isSleep = false;

                for (int i = 0; i < m_WheelColliders.Length; i++)
                {
                    m_WheelColliders[i].GetGroundHit(out wheelHit);

                    if (m_WheelColliders[i].isGrounded == true)
                    {
                        if(wheelHit.forwardSlip > m_ForwardSlipLimit || wheelHit.sidewaysSlip > m_SidewaySlipLimit)
                        {
                            if (m_SkidTrail[i] == null)
                                m_SkidTrail[i] = Instantiate(m_SkidPrefab).transform;

                            if (m_Audio.isPlaying == false)
                                m_Audio.Play();

                            if (m_SkidTrail[i] != null)
                            {
                                m_SkidTrail[i].position = m_WheelColliders[i].transform.position - wheelHit.normal * m_WheelColliders[i].radius;
                                m_SkidTrail[i].forward  = -wheelHit.normal;

                                m_Smoke[i].transform.position = m_SkidTrail[i].position;
                                m_Smoke[i].Emit(1);
                            }

                            isSleep = true;
                            continue;
                        }
                    }
                    else
                    {
                        m_SkidTrail[i] = null;
                        m_Smoke[i].Stop();
                    }
                }

                if(isSleep == false)
                    m_Audio.Stop();
            }
        }
    }
}



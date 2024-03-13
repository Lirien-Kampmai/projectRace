using ProjectCar.Car;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace SFX
    {
        [RequireComponent(typeof(AudioSource))]
        public class EngineSound : MonoBehaviour, IDependency<CarInfoModel>
        {
            public void CreateDependency(CarInfoModel obj) => m_Car = obj;

            [SerializeField] private AudioSource m_AudioSource;
            [SerializeField] private float m_ModVolume;
            [SerializeField] private float m_ModPitch;
            [SerializeField] private float m_ModRPM;
            [SerializeField] private float m_BasePitch  = 1.0f;
            [SerializeField] private float m_BaseVolume = 0.4f;

            private CarInfoModel m_Car;

            private void Start()
            {
                m_AudioSource = GetComponent        <AudioSource>();
            }

            private void Update()
            {
                PitchControll();
                VolumeControll();
            }

            private void PitchControll()
            {
                m_AudioSource.pitch = m_BasePitch + m_ModPitch * ((m_Car.EngineRPM / m_Car.EngineMaxRPM) * m_ModRPM);
            }

            private void VolumeControll()
            {
                m_AudioSource.volume = m_BaseVolume + m_ModVolume * (m_Car.EngineRPM / m_Car.EngineMaxRPM);
            }
        }
    }
}



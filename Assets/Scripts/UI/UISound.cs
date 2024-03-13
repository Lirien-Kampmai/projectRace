using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectCar
{
    namespace UI
    {
        [RequireComponent(typeof(AudioSource))]
        public class UISound : MonoBehaviour
        {
            [SerializeField] private AudioClip hover;
            [SerializeField] private AudioClip click;

            private new AudioSource audio;

            private UIButton[] uiButton;

            private void Start()
            {
                audio = GetComponent<AudioSource>();

                uiButton = GetComponentsInChildren<UIButton>(true);

                for (int i = 0; i < uiButton.Length; i++)
                {
                    uiButton[i].PointerEnter += OnPointEnter;
                    uiButton[i].PointerClick += OnPointClicked;
                }

            }

            private void OnDestroy()
            {
                for (int i = 0; i < uiButton.Length; i++)
                {
                    uiButton[i].PointerEnter -= OnPointEnter;
                    uiButton[i].PointerClick -= OnPointClicked;
                }
            }

            private void OnPointClicked(UIButton arg0)
            {
                audio.PlayOneShot(click);
            }

            private void OnPointEnter(UIButton arg0)
            {
                audio.PlayOneShot(hover);
            }
        }
    }
}



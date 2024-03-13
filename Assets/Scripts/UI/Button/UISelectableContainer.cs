using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectCar
{
    namespace UI
    {
        public class UISelectableContainer : MonoBehaviour
        {
            [SerializeField] private Transform m_ButtonsContainer;

            public bool Interactable = true;
            public void SetInteractable(bool interactable) => Interactable = interactable;

            private UISelectableButton[] buttons;

            private int selectButtonsIndex = 0;

            private void Start()
            {
                if (Interactable == false) return;

                buttons = m_ButtonsContainer.GetComponentsInChildren<UISelectableButton>();

                if (buttons == null) Debug.LogError("Buttons list empty");

                for(int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].PointerEnter += OnPointEnter;
                }

                buttons[selectButtonsIndex].SetOnFocus();
            }

            private void OnDestroy()
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].PointerEnter -= OnPointEnter;
                }
            }

            private void OnPointEnter(UIButton button)
            {
                SelectButton(button);
            }

            private void SelectButton(UIButton button)
            {
                if (Interactable == false) return;

                buttons[selectButtonsIndex].SetOffFocus();

                for (int i = 0; i < buttons.Length; i++)
                {
                    if(button == buttons[i])
                    {
                        selectButtonsIndex = i;
                        button.SetOnFocus();
                        break;
                    }
                }
            }
        }
    }
}



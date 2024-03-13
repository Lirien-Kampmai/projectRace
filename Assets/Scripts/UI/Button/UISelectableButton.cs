using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectCar
{
    namespace UI
    {
        public class UISelectableButton : UIButton
        {
            [SerializeField] private Image m_SelectImage;

            public UnityEvent OnSelect;
            public UnityEvent OnUnSelect;

            public override void SetOnFocus()
            {
                base.SetOnFocus();

                m_SelectImage.enabled = true;
                OnSelect?.Invoke();
            }

            public override void SetOffFocus()
            {
                base.SetOffFocus();

                m_SelectImage.enabled = false;
                OnUnSelect?.Invoke();
            }

        }
    }
}



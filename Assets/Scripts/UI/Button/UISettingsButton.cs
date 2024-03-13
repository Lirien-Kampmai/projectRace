using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCar
{
    namespace UI
    {
        public class UISettingsButton : UISelectableButton
        {
            [SerializeField] private Settings setting;

            [SerializeField] private Text titleText;
            [SerializeField] private Text valueText;

            [SerializeField] private Image nextImage;
            [SerializeField] private Image previosImage;

            public void SetNextSetting()
            {
                setting?.SetNextValue();
                setting?.Apply();
                UpdateInfo();
            }

            public void SetPreviosSetting()
            {
                setting?.SetPreviosValue();
                setting?.Apply();
                UpdateInfo();
            }

            private void Start()
            {
                ApplyProperty(setting);
            }

            private void UpdateInfo()
            {
                titleText.text = setting.Title;
                valueText.text = setting.GetStringValue();

                nextImage.enabled = !setting.isMinValue;
                previosImage.enabled = !setting.isMaxValue;
            }

            public void ApplyProperty(Settings property)
            {
                if (property == null) return;

                setting = property;

                UpdateInfo();
            }
        }
    }
}

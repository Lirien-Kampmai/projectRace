using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QalitySetting : Settings
{
    private int currentQalityLevelIndex = 0;

    public override bool isMinValue { get => currentQalityLevelIndex == 0; }
    public override bool isMaxValue { get => currentQalityLevelIndex == QualitySettings.names.Length - 1; }


    public override void SetNextValue()
    {
        if(isMaxValue == false)
            currentQalityLevelIndex++;
    }

    public override void SetPreviosValue()
    {
        if (isMinValue == false)
            currentQalityLevelIndex--;
    }

    public override object GetValue()
    {
        return QualitySettings.names[currentQalityLevelIndex];
    }

    public override string GetStringValue()
    {
        return QualitySettings.names[currentQalityLevelIndex];
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(currentQalityLevelIndex);

        Save();
    }

    public override void Load()
    {
        currentQalityLevelIndex = PlayerPrefs.GetInt(title, QualitySettings.names.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentQalityLevelIndex);
    }

}
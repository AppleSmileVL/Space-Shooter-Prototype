using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerFloatSetting : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameParametry;

    [SerializeField] private float minRealValue;
    [SerializeField] private float maxRealValue;

    [SerializeField] private float virtualStep;
    [SerializeField] private float minVirtualValue;
    [SerializeField] private float maxVirtualValue;

    private float currentValue;
    private bool isInitialized = false;

    public override bool isMinValue { get => currentValue == minRealValue; }
    public override bool isMaxValue { get => currentValue == maxRealValue; }

    private void Initialize()
    {
        if (!isInitialized && audioMixer != null)
        {
            audioMixer.GetFloat(nameParametry, out float value);
            currentValue = value;
            isInitialized = true;
        }
    }

    public override void SetNextValue()
    {
        Initialize();
        AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override void SetPreviousValue()
    {
        Initialize();
        AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override string GetStringValue()
    {
        Initialize();
        return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
    }

    public override object GetValue()
    {
        Initialize();
        return currentValue;
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
    }

    public override void Apply()
    {
        Initialize();
        audioMixer.SetFloat(nameParametry, currentValue);
        Save();
    }

    public override void Save()
    {
        PlayerPrefs.SetFloat("Setting_" + nameParametry, currentValue);
        PlayerPrefs.Save();
    }

    public override void Load()
    {
        if (PlayerPrefs.HasKey("Setting_" + nameParametry))
        {
            currentValue = PlayerPrefs.GetFloat("Setting_" + nameParametry);
            isInitialized = true;
        }
    }
}

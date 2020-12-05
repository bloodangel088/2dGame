using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volume;
    [SerializeField] private AudioMixer audioMixer;
    [Space]
    [SerializeField] private Toggle fullScrean;
    [Space]
    [SerializeField] TMP_Dropdown resolutionDropdown;
    private Resolution[] availableResolution;
    [Space]
    [SerializeField] TMP_Dropdown qualityDropdown;
    private string[] qualityLevels;


    void Start()
    {
        volume.onValueChanged.AddListener(OnVolumeChanged);
        fullScrean.onValueChanged.AddListener(OnFullScreanChanged);
        qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        availableResolution = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < availableResolution.Length; i++)
        {
            if (availableResolution[i].width <= 800)
                continue;
            options.Add(availableResolution[i].width + "x" + availableResolution[i].height);
            if (availableResolution[i].width == Screen.currentResolution.width && availableResolution[i].height == Screen.currentResolution.height)
                currentIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
        qualityDropdown.ClearOptions();
        qualityLevels = QualitySettings.names;
        qualityDropdown.AddOptions(qualityLevels.ToList());
        int qualityLvl = QualitySettings.GetQualityLevel();
        qualityDropdown.value = qualityLvl;
        qualityDropdown.RefreshShownValue();
    }

    private void Destroy()
    {
        volume.onValueChanged.RemoveListener(OnVolumeChanged);
        fullScrean.onValueChanged.RemoveListener(OnFullScreanChanged);
        resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChanged);
        qualityDropdown.onValueChanged.RemoveListener(OnQualityChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    private void OnFullScreanChanged(bool value)
    {
        Screen.fullScreen = value;
    }

    private void OnResolutionChanged(int resolutionIndex)
    {
        Resolution resolution = availableResolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void OnQualityChanged(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel, true);
    }
}

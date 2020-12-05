using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour
{
    protected LvLManager lvlManager;
    protected UIAudioManager audioManager;

    [SerializeField] protected GameObject menu;

    [Header ("Menu buttons")]
    [SerializeField] protected UnityEngine.UI.Button play;
    [SerializeField] protected UnityEngine.UI.Button settings;
    [SerializeField] protected UnityEngine.UI.Button quit;

    [Header("Settings menu")]
    [SerializeField] protected GameObject settingsMenu;
    [SerializeField] protected UnityEngine.UI.Button closeSettings;

    protected virtual void Start()
    {
        lvlManager = LvLManager.Instanse;
        audioManager = UIAudioManager.Instance;
        quit.onClick.AddListener(OnQuitClicked);
        settings.onClick.AddListener(OnSettingsCliscked);
        closeSettings.onClick.AddListener(OnSettingsCliscked);
    }

    private void OnSettingsCliscked()
    {
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);

        //audioManager.Play(ClipName.Settings);
    }

    protected virtual void OnDestroy()
    {
        quit.onClick.RemoveListener(OnQuitClicked);
        settings.onClick.RemoveListener(OnSettingsCliscked);
        closeSettings.onClick.RemoveListener(OnSettingsCliscked);
    }

    protected virtual void ChangeMenuStatus()
    {
        menu.SetActive(!menu.activeInHierarchy);
        Time.timeScale = menu.activeInHierarchy ? 0 : 1;
    }

    private void OnQuitClicked()
    {
        lvlManager.Quit();
        //audioManager.Play(ClipName.Quit);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenu : MonoBehaviour
{
    protected LvLManager lvlManager;

    [SerializeField] protected GameObject menu;

    [Header ("Menu buttons")]
    [SerializeField] protected UnityEngine.UI.Button play;
    [SerializeField] protected UnityEngine.UI.Button settings;
    [SerializeField] protected UnityEngine.UI.Button quit;

    protected virtual void Start()
    {
        lvlManager = LvLManager.Instanse;
        
        quit.onClick.AddListener(lvlManager.Quit);
    }

    protected virtual void OnDestroy()
    {
        //quit.onClick.RemoveListener(lvlManager.Quit);
    }

    protected virtual void ChangeMenuStatus()
    {
        menu.SetActive(!menu.activeInHierarchy);
        Time.timeScale = menu.activeInHierarchy ? 0 : 1;
    }
}

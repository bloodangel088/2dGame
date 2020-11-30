using System.Security.Cryptography;
using UnityEngine;


public class Menu : BaseMenu
{
    [SerializeField] private UnityEngine.UI.Button restart;
    [SerializeField] private UnityEngine.UI.Button backToMenu;


    protected override void Start()
    {
        base.Start();
        play.onClick.AddListener(ChangeMenuStatus);
        restart.onClick.AddListener(lvlManager.Restart);
        backToMenu.onClick.AddListener(GoToMainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            ChangeMenuStatus();
    }

    protected override void OnDestroy()
    {
        play.onClick.RemoveListener(ChangeMenuStatus);
        restart.onClick.RemoveListener(lvlManager.Restart);
        backToMenu.onClick.RemoveListener(GoToMainMenu);
    }

    protected override void ChangeMenuStatus()
    {
        base.ChangeMenuStatus();
        Time.timeScale = menu.activeInHierarchy ? 0 : 1;
    }

    public void GoToMainMenu()
    {
        LvLManager.Instanse.ChangeLvL((int)Scenes.MainMenu);
    }
}

using TMPro;
using UnityEngine;

public class MainMenu : BaseMenu
{
    [SerializeField] private UnityEngine.UI.Button choseLvL;
    [SerializeField] private UnityEngine.UI.Button reset;
    [SerializeField] private UnityEngine.UI.Button close;

    [SerializeField] private GameObject lvlMenu;

    private TMP_Text playbutton;
    private int lvl = 1;

    protected override void Start()
    {
        base.Start();
        LvLManager.NewGame.AddListener(() =>
        {
            Debug.Log("New game!");
            play.GetComponentInChildren<TMP_Text>().text = "New game";
            lvl = 1;
        });

        lvl = LvLManager.GetLastLevelIndex();
       
        if(lvl > 1)
        {
            play.GetComponentInChildren<TMP_Text>().text = "Play";
        }

        choseLvL.onClick.AddListener(UseLvlMenu);
        close.onClick.AddListener(UseLvlMenu);
        reset.onClick.AddListener(Reset);
        play.onClick.AddListener(Play);
    }

    private void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        lvl = LvLManager.GetLastLevelIndex();
        lvlManager.Restart();
        
    }

    private void UseLvlMenu()
    {
        lvlMenu.SetActive(!lvlMenu.activeInHierarchy);
        ChangeMenuStatus();
    }

    private void Play()
    {
        lvlManager.ChangeLvL(lvl);
        //audioManager.Play(ClipName.Play);
    }
}

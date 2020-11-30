using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LvLManager : MonoBehaviour
{
    #region Singltone
    public static LvLManager Instanse;
    private void Awake()
    {
        if (Instanse == null)
            Instanse = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public static UnityEvent NewGame = new UnityEvent();
    public static int CurrentScene => SceneManager.GetActiveScene().buildIndex;
    public static bool HaveNextScene => Application.CanStreamedLevelBeLoaded(CurrentScene+1);

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        ChangeLvL(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnTriggerEnter2D()
    {
        EndLvL();
    }

    public void EndLvL()
    {
        if (HaveNextScene)
            ChangeLvL(SceneManager.GetActiveScene().buildIndex + 1);
        else
            ChangeLvL(0);
    }

    public void ChangeLvL(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public static int GetLastLevelIndex()
    {
        if (PlayerPrefs.HasKey(GamePrefs.LastLevel.ToString()) == false)
        {
            PlayerPrefs.SetInt(GamePrefs.LastLevel.ToString(), 1);
            NewGame?.Invoke();
            PlayerPrefs.Save();
            return 1;
        }
        else return PlayerPrefs.GetInt(GamePrefs.LastLevel.ToString());
    }

    public static int TryUpdateLevel()
    {
        int lastSceneIndex = GetLastLevelIndex();

        if (lastSceneIndex < CurrentScene)
        {
            PlayerPrefs.SetInt(GamePrefs.LastLevel.ToString(), CurrentScene);
            return 1;
        }
        return CurrentScene > 0 ? CurrentScene : 1;
    }

}
public enum Scenes
{
    MainMenu,
    first,
    second,
    third,
}
public enum GamePrefs
{
    LastLevel,
    LvlPlayed,
}

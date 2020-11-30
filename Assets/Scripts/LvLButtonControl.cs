using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvLButtonControl : MonoBehaviour
{
    private UnityEngine.UI.Button button;

    [SerializeField] private Scenes scene;

    void Start()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        if (LvLManager.GetLastLevelIndex() < (int)scene)
        {
            button.interactable = false;
            GetComponentInChildren<TMP_Text>().text = ((int)scene).ToString();
            return;
        }

        button.interactable = true;
        button.onClick.AddListener(ChangeLvL);
        GetComponentInChildren<TMP_Text>().text = ((int)scene).ToString();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    void ChangeLvL()
    {
        LvLManager.Instanse.ChangeLvL((int)scene);
    }
}
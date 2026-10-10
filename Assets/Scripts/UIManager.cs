using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private GameObject _main_menu_panel;
    private GameObject _in_game_overlay;

    void Start()
    {
        // Stick with one instance of UIManager.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Gameplay-1");
        _main_menu_panel.SetActive(false);
        _in_game_overlay.SetActive(true);
    }

    public void OnBackToMain()
    {
        SceneManager.LoadScene("Menu-UI");
        _main_menu_panel.SetActive(true);
        _in_game_overlay.SetActive(false);
    }
}

using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject _main_menu_panel;

    [SerializeField]
    private GameObject _in_game_overlay;

    [SerializeField]
    private GameObject _pause_menu_panel;

    private bool paused = false;

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

        _pause_menu_panel.SetActive(false);
    }

    void Update()
    {
        if (false)
        {
            if (!paused)
                OnPause();
            else
                OnUnpause();

            paused = !paused;
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
        _pause_menu_panel.SetActive(false);
    }

    public void OnBackToMain()
    {
        SceneManager.LoadScene("Menu-UI");
        _main_menu_panel.SetActive(true);
        _in_game_overlay.SetActive(false);
        _pause_menu_panel.SetActive(false);
    }

    public void OnPause()
    {
        if (_main_menu_panel.activeSelf)
            return;

        _pause_menu_panel.SetActive(true);
    }

    public void OnUnpause()
    {
        if (_main_menu_panel.activeSelf)
            return;

        _pause_menu_panel.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject _mainMenuPanel;

    [SerializeField]
    private GameObject _inGameOverlay;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    void Start()
    {
        // Stick with one instance of UIManager, can be used in multiple scenes anyway.
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

        // Make sure the pause menu panel is always inactive at the beginning.
        _pauseMenuPanel.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Gameplay-3D");
        _mainMenuPanel.SetActive(false);
        _inGameOverlay.SetActive(true);
        _pauseMenuPanel.SetActive(false);
    }

    public void OnBackToMain()
    {
        GameStateManager.instance.TogglePause();
        SceneManager.LoadScene("Menu-UI");
        _mainMenuPanel.SetActive(true);
        _inGameOverlay.SetActive(false);
        _pauseMenuPanel.SetActive(false);
    }

    public void OnPause()
    {
        if (_mainMenuPanel.activeSelf)
            return;

        _pauseMenuPanel.SetActive(true);
    }

    public void OnUnpause()
    {
        if (_mainMenuPanel.activeSelf)
            return;

        GameStateManager.instance.isPaused = false;

        _pauseMenuPanel.SetActive(false);
    }
    public void BackButton()
    {
        if (_mainMenuPanel.activeSelf)
            return;
        GameStateManager.instance.UnPauseTime();
        _pauseMenuPanel.SetActive(false);
    }
}

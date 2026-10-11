using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject _mainMenuPanel;

    [SerializeField]
    private GameObject _inGameOverlay;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    [SerializeField]
    private GameObject _winPanel;

        [SerializeField]
    private GameObject _thankYouPanel;

    void Start()
    {
        // Stick with one instance of UIManager, can be used in multiple scenes anyway.
        if (Instance == null)
        {
            Instance = this;
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

    public void OnWin()
    {
        StartCoroutine(WaitAndMainMenu());
    }

    private IEnumerator WaitAndMainMenu()
    {
        _winPanel.SetActive(true);
        _winPanel.GetComponent<VideoPlayer>().Play();
        yield return new WaitForSeconds(8);
        _winPanel.SetActive(false);
        _thankYouPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        OnBackToMain();
    }
}

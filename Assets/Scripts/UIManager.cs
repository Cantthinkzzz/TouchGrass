using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private GameObject _rainAgainPanel;

    [SerializeField]
    private float _fadeDuration = 1.0f;

    [SerializeField]
    private float _displayDuration = 3.0f;

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

        // Make sure the pause menu panel and rain again panels are always inactive at the beginning.
        _pauseMenuPanel.SetActive(false);
        if (_rainAgainPanel != null)
            _rainAgainPanel.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnPlay()
    {
        // Rain coroutine.
        if (_rainAgainPanel != null)
        {
            StartCoroutine(ShowAndFadeRainPanel());
        }

        SceneManager.LoadScene("Gameplay-3D");
        _mainMenuPanel.SetActive(false);
        _inGameOverlay.SetActive(true);
        _pauseMenuPanel.SetActive(false);
    }

    private IEnumerator ShowAndFadeRainPanel()
    {
        _rainAgainPanel.SetActive(true);

        CanvasGroup canvasGroup = _rainAgainPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = _rainAgainPanel.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(_displayDuration);

        float elapsedTime = 0f;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        _rainAgainPanel.SetActive(false);
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

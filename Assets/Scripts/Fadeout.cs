using System.Collections;
using UnityEngine;

public class Fadeout : MonoBehaviour
{
    [SerializeField]
    private float _fadeDuration = 1.0f;

    [SerializeField]
    private float _displayDuration = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShowAndFadeRainPanel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowAndFadeRainPanel()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
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
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class Monochrome : MonoBehaviour
{
    public static Monochrome Instance;

    [SerializeField] private Material[] _monochromeMaterials;
    [SerializeField] private Material _sensoryInteractablesMaterial;
    [SerializeField, Range(0.1f, 5f)] private float _lerpSpeed = 5f; // Speed of the transition

    private static readonly int MonochromeBlendID = Shader.PropertyToID("_MonochromeBlend");

    [Range(0f, 1f)] public float Color = 0f;
    private float _targetColor = 0f;
    private bool _sensoryColorful = false;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        _targetColor = Color;
        ApplyBlendValue(Color);
    }

    private void Update()
    {
        if (!Mathf.Approximately(Color, _targetColor))
        {
            Color = Mathf.Lerp(Color, _targetColor, Time.deltaTime * _lerpSpeed);
            ApplyBlendValue(Color);
        }
    }

    private void OnValidate()
    {
        _targetColor = Color;
        ApplyBlendValue(Color);
    }

    public void SetColor(float targetValue)
    {
        _targetColor = targetValue;
    }

    public void MakeSensoryColorful()
    {
        _sensoryColorful = true;
        if (_sensoryInteractablesMaterial != null)
        {
            _sensoryInteractablesMaterial.SetFloat(MonochromeBlendID, 1.0f);
        }
    }

    public void ApplyBlendValue(float value)
    {
        foreach (var mat in _monochromeMaterials)
        {
            if (mat != null)
            {
                mat.SetFloat(MonochromeBlendID, value);
            }
        }

        if (_sensoryInteractablesMaterial != null && !_sensoryColorful)
        {
            _sensoryInteractablesMaterial.SetFloat(MonochromeBlendID, value);
        }
    }
}
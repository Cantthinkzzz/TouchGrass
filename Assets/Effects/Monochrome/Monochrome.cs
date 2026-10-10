using UnityEngine;

public class Monochrome : MonoBehaviour
{
    public static Monochrome Instance;

    [SerializeField] private Material[] _monochromeMaterials;
    [SerializeField] private Material _sensoryInteractablesMaterial;

    private static readonly int MonochromeBlendID = Shader.PropertyToID("_MonochromeBlend");

    [Range(0f, 1f)] public float Color = 0f;
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
        }

        ApplyBlendValue(Color);
    }

    private void OnValidate()
    {
        ApplyBlendValue(Color);
    }

    // Call this when you need to make the sensory colorful, after getting all visual.
    public void MakeSensoryColorful()
    {
        _sensoryColorful = true;
        if (_sensoryInteractablesMaterial != null || _sensoryColorful)
        {
            _sensoryInteractablesMaterial.SetFloat(MonochromeBlendID, 1.0f);
        }
    }

    private void ApplyBlendValue(float value)
    {
        foreach (var mat in _monochromeMaterials)
        {
            if (mat != null)
            {
                mat.SetFloat(MonochromeBlendID, value);
            }
        }

        if (_sensoryInteractablesMaterial != null || _sensoryColorful)
        {
            _sensoryInteractablesMaterial.SetFloat(MonochromeBlendID, value);
        }
    }
}
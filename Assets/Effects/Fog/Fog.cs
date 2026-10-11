using UnityEngine;

[ExecuteAlways]
public class Fog : MonoBehaviour
{
    public static Fog Instance;

    [SerializeField] private Transform _player;

    [Header("Settings")]
    public bool Enabled = true;

    [Range(0f, 1f)]
    [SerializeField] private float _radiusExtension = 0f;
    private float _targetRadiusExtension = 0f;

    [SerializeField, Range(0.1f, 10f)] private float _lerpSpeed = 5f;

    [Min(0f)]
    [SerializeField] private float _startRadius = 5f;

    [Min(0f)]
    [SerializeField] private float _endRadius = 20f;

    [Min(0.01f)]
    [SerializeField] private float _edgeSoftness = 2f;

    [SerializeField] private Color _fogColor = new Color(0f, 0f, 0f, 1f);

    private static readonly int FogEnabledID = Shader.PropertyToID("_FogEnabled");
    private static readonly int PlayerPositionID = Shader.PropertyToID("_FogPlayerPosition");
    private static readonly int RadiusExtensionID = Shader.PropertyToID("_RadiusExtension");
    private static readonly int StartRadiusID = Shader.PropertyToID("_StartRadius");
    private static readonly int EndRadiusID = Shader.PropertyToID("_EndRadius");
    private static readonly int EdgeSoftnessID = Shader.PropertyToID("_EdgeSoftness");
    private static readonly int FogColorID = Shader.PropertyToID("_FogColor");

    private void OnEnable()
    {
        _targetRadiusExtension = _radiusExtension;
        UpdateFog();
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _targetRadiusExtension = _radiusExtension;
    }

    private void Update()
    {
        // Smoothly lerp towards the target extension during gameplay
        if (Application.isPlaying && !Mathf.Approximately(_radiusExtension, _targetRadiusExtension))
        {
            _radiusExtension = Mathf.Lerp(_radiusExtension, _targetRadiusExtension, Time.deltaTime * _lerpSpeed);
        }

        UpdateFog();
    }

    private void OnValidate()
    {
        _targetRadiusExtension = _radiusExtension;
        UpdateFog();
    }

    private void UpdateFog()
    {
        Shader.SetGlobalFloat(FogEnabledID, Enabled ? 1f : 0f);

        if (_player != null)
        {
            Shader.SetGlobalVector(PlayerPositionID, _player.position + new Vector3(0, 0, 1f));
        }

        Shader.SetGlobalFloat(RadiusExtensionID, _radiusExtension);
        Shader.SetGlobalFloat(StartRadiusID, _startRadius);
        Shader.SetGlobalFloat(EndRadiusID, _endRadius);
        Shader.SetGlobalFloat(EdgeSoftnessID, _edgeSoftness);
        Shader.SetGlobalColor(FogColorID, _fogColor);
    }

    public void SetFog(float targetValue)
    {
        _targetRadiusExtension = targetValue;
    }
}
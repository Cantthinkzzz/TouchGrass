using UnityEngine;

[ExecuteAlways]
public class Fog : MonoBehaviour
{
    public static Fog Instance;

    [SerializeField] private Transform _player;

    [Range(0f, 1f)]
    [SerializeField] private float _radiusExtension = 0f;

    [Min(0f)]
    [SerializeField] private float _startRadius = 5f;

    [Min(0f)]
    [SerializeField] private float _endRadius = 20f;

    [Min(0.01f)]
    [SerializeField] private float _edgeSoftness = 2f;

    [SerializeField] private Color _fogColor = new Color(0f, 0f, 0f, 1f);

    private static readonly int PlayerPositionID =
        Shader.PropertyToID("_FogPlayerPosition");

    private static readonly int RadiusExtensionID =
        Shader.PropertyToID("_RadiusExtension");

    private static readonly int StartRadiusID =
        Shader.PropertyToID("_StartRadius");

    private static readonly int EndRadiusID =
        Shader.PropertyToID("_EndRadius");

    private static readonly int EdgeSoftnessID =
        Shader.PropertyToID("_EdgeSoftness");

    private static readonly int FogColorID =
        Shader.PropertyToID("_FogColor");

    private void OnEnable()
    {
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
        }
    }

    private void LateUpdate()
    {
        UpdateFog();
    }

    private void OnValidate()
    {
        UpdateFog();
    }

    private void UpdateFog()
    {
        // Fallback or editor check if player is not assigned
        if (_player != null)
        {
            Shader.SetGlobalVector(
                PlayerPositionID,
                _player.position + new Vector3(0, 0, 1f)
            );
        }

        Shader.SetGlobalFloat(
            RadiusExtensionID,
            _radiusExtension
        );

        Shader.SetGlobalFloat(
            StartRadiusID,
            _startRadius
        );

        Shader.SetGlobalFloat(
            EndRadiusID,
            _endRadius
        );

        Shader.SetGlobalFloat(
            EdgeSoftnessID,
            _edgeSoftness
        );

        Shader.SetGlobalColor(
            FogColorID,
            _fogColor
        );
    }
}
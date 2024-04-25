using UnityEngine;
using UnityEngine.UI;

public class VersionWatermark : MonoBehaviour
{
    [Header("Version Watermark Settings")]
    public bool show;

    private Text waterwarkText;

    private void Awake()
    {
        InitializeComponents();
    }
    private void InitializeComponents()
    {
        waterwarkText = GetComponent<Text>();
    }

    private void Start()
    {
        SetVersionWatermark();
    }

    private void SetVersionWatermark()
    {
        if (show)
            waterwarkText.text = "v" + Application.version;
    }
}
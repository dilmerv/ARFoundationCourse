using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARLightEstimation : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI averageBrightnessText;

    [SerializeField]
    private TextMeshProUGUI colorTemperatureText;

    [SerializeField]
    private TextMeshProUGUI colorCorrectionText;

    private Light arLight;

    private ARCameraManager arCameraManager;

    private float? brightness;

    private float? colorTemperature;

    private Color? colorCorrection;

    private void Awake()
    {
        arLight = FindObjectOfType<Light>();
        arCameraManager = FindObjectOfType<ARCameraManager>();

        PopulateLightEstimateValues();
    }

    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameReceived;
    }

    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameReceived;
    }

    private void FrameReceived(ARCameraFrameEventArgs obj)
    {
        Logger.Instance.LogInfo("AR Camera frame received");

        if (obj.lightEstimation.averageBrightness.HasValue)
        {
            brightness = obj.lightEstimation.averageBrightness;
            arLight.intensity = brightness.Value;
        }
        if (obj.lightEstimation.averageColorTemperature.HasValue)
        {
            colorTemperature = obj.lightEstimation.averageColorTemperature;
            arLight.colorTemperature = colorTemperature.Value;
        }
        if (obj.lightEstimation.colorCorrection.HasValue)
        {
            colorCorrection = obj.lightEstimation.colorCorrection;
            arLight.color = colorCorrection.Value;
        }
        PopulateLightEstimateValues();
    }

    private void PopulateLightEstimateValues()
    {
        averageBrightnessText.text = $"Average Brightness: {(!brightness.HasValue ? "Not Set" : brightness.Value)}";
        colorTemperatureText.text = $"Color Temperature: {(!colorTemperature.HasValue ? "Not Set" : colorTemperature.Value)}";
        colorCorrectionText.text = $"Color Correction: {(!colorCorrection.HasValue ? "Not Set" : colorCorrection.Value)}";
    }
}

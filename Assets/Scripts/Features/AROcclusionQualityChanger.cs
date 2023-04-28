using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(AROcclusionManager))]
public class AROcclusionQualityChanger : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown depthQualityDropdown;

    private AROcclusionManager occlusionManager;

    private void Awake()
    {
        occlusionManager = GetComponent<AROcclusionManager>();
        depthQualityDropdown.onValueChanged.AddListener(quality =>
        {
            EnvironmentDepthMode depthMode = (EnvironmentDepthMode)quality;
            occlusionManager.requestedEnvironmentDepthMode = depthMode;
            Logger.Instance.LogInfo($"Depth Mode set to: {depthMode}");
        });
    }
}

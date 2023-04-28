using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(AROcclusionManager))]
public class AROcclusionSupport : MonoBehaviour
{
    private AROcclusionManager arOcclusionManager;

    private const string UNSUPPORTED_PREFIX = "Feature not supported:";

    private const string SUPPORTED_PREFIX = "Feature available:";

    private void Start()
    {
        arOcclusionManager = GetComponent<AROcclusionManager>();
        List<string> unsupportedAreas = new List<string>();

        bool notSupported = arOcclusionManager.descriptor == null;

        if (notSupported || arOcclusionManager.descriptor?.environmentDepthConfidenceImageSupported == Supported.Unsupported)
            unsupportedAreas.Add("DepthConfidenceImage");
        else
            Logger.Instance.LogInfo($"{SUPPORTED_PREFIX} DepthConfidenceImage");

        if (notSupported || arOcclusionManager.descriptor?.environmentDepthImageSupported == Supported.Unsupported)
            unsupportedAreas.Add("DepthImage");
        else
            Logger.Instance.LogInfo($"{SUPPORTED_PREFIX} DepthImage");

        if (notSupported || arOcclusionManager.descriptor?.environmentDepthTemporalSmoothingSupported == Supported.Unsupported)
            unsupportedAreas.Add("DepthTemporalSmoothing");
        else
            Logger.Instance.LogInfo($"{SUPPORTED_PREFIX} DepthTemporalSmoothing");

        if (unsupportedAreas.Count > 0)
            Logger.Instance.LogWarning($"{UNSUPPORTED_PREFIX} {string.Join(",", unsupportedAreas)}");
    }
}

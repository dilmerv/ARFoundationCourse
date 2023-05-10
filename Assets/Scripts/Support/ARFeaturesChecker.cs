using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFeaturesChecker : MonoBehaviour
{
    void Start()
    {
        // check support for AR Features
        var planeManager = FindObjectOfType<ARPlaneManager>();
        if (planeManager != null)
        {
            var isPlaneDetectionSupported = planeManager.subsystem != null;
            if (!isPlaneDetectionSupported)
            {
                Logger.Instance.LogInfo("Plane Detection is not supported on this device");
            }
            else
            {
                Logger.Instance.LogInfo("Plane Detection is supported on this device");
                Logger.Instance.LogInfo($"Plane Detection feature(horizontal planes={planeManager.subsystem.subsystemDescriptor.supportsHorizontalPlaneDetection})");
                Logger.Instance.LogInfo($"Plane Detection feature(vertical planes={planeManager.subsystem.subsystemDescriptor.supportsVerticalPlaneDetection})");
                Logger.Instance.LogInfo($"Plane Detection feature(classification planes={planeManager.subsystem.subsystemDescriptor.supportsClassification})");
            }
        }

        var faceManager = FindObjectOfType<ARFaceManager>();
        if (faceManager != null)
        {
            var faceTrackingSupported = faceManager.subsystem != null;
            if (!faceTrackingSupported)
            {
                Logger.Instance.LogInfo("Face Tracking is not supported on this device");
            }
            else
            {
                Logger.Instance.LogInfo("Face Tracking is supported on this device");
                Logger.Instance.LogInfo($"Face Tracking feature(eye tracking={faceManager.subsystem.subsystemDescriptor.supportsEyeTracking})");
                Logger.Instance.LogInfo($"Face Tracking feature(face pose={faceManager.subsystem.subsystemDescriptor.supportsFacePose})");
                Logger.Instance.LogInfo($"Face Tracking feature(face mesh normals={faceManager.subsystem.subsystemDescriptor.supportsFaceMeshNormals})");
                Logger.Instance.LogInfo($"Face Tracking feature(face mesh vertices and indices={faceManager.subsystem.subsystemDescriptor.supportsFaceMeshVerticesAndIndices})");
            }
        }

        var pointCloudManager = FindObjectOfType<ARPointCloudManager>();
        if(pointCloudManager != null)
        {
            var featurePointsSupported = pointCloudManager.subsystem != null;
            if (!featurePointsSupported)
            {
                Logger.Instance.LogInfo("Feature Points is not supported on this device");
            }
            else
            {
                Logger.Instance.LogInfo("Feature Points is supported on this device");
                Logger.Instance.LogInfo($"Feature Points feature(feature points={pointCloudManager.subsystem.subsystemDescriptor.supportsFeaturePoints})");
                Logger.Instance.LogInfo($"Feature Points feature(confidence={pointCloudManager.subsystem.subsystemDescriptor.supportsConfidence})");
            }
        }
    }
}

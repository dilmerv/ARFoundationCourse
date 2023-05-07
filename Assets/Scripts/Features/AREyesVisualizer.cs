using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AREyesVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject eyesPrefab;

    private ARFaceManager faceManager;

    private ARFace face;
    
    private GameObject leftEye;

    private GameObject rightEye;

    void Start()
    {
        faceManager = FindAnyObjectByType<ARFaceManager>();
        face = GetComponent<ARFace>();
        face.updated += FaceUpdated;

        if (faceManager.descriptor.supportsEyeTracking)
        {
            Logger.Instance.LogInfo($"Eye tracking is supported on this device");
        }
        else
        {
            Logger.Instance.LogInfo($"Eye tracking is not currently supported on this device");
        }
    }

    private void OnDisable()
    {
        face.updated -= FaceUpdated;
    }

    void FaceUpdated(ARFaceUpdatedEventArgs faceUpdatedEventArgs)
    {
        CreateEyesIfNeeded(faceUpdatedEventArgs);
    }

    private void CreateEyesIfNeeded(ARFaceUpdatedEventArgs faceUpdatedEventArgs)
    {
        if (!faceManager.descriptor.supportsEyeTracking) return;

        if (leftEye == null)
        {
            leftEye = Instantiate(eyesPrefab, faceUpdatedEventArgs.face.leftEye);
            Logger.Instance.LogInfo($"Left Eye created");
        }
        if (rightEye == null)
        {
            rightEye = Instantiate(eyesPrefab, faceUpdatedEventArgs.face.rightEye);
            Logger.Instance.LogInfo($"Right Eye created");
        }
    }
}

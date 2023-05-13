using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AREyesVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject eyesPrefab;

    private TextMeshProUGUI eyeInfo;

    private ARFaceManager faceManager;

    private ARFace face;
    
    private GameObject leftEye;

    private GameObject rightEye;

    private void Awake()
    {
        eyeInfo = GameObject.Find("DebugEyesUI").GetComponentInChildren<TextMeshProUGUI>();
        eyeInfo.text = string.Empty;
    }

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

        eyeInfo.text = $"Left Eye POS/ROT: {leftEye.transform.position}|{leftEye.transform.rotation}\n" +
            $"Right Eye POS/ROT: {rightEye.transform.position}|{rightEye.transform.rotation}";
    }
}

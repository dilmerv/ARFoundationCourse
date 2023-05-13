using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;

public class ARFaceBlendShapesMapper : MonoBehaviour
{
    [SerializeField]
    private ARFaceBlendShapesMapping blendShapesMapping = null;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer = null;

    private ARKitFaceSubsystem arKitFaceSubsystem;

    private Dictionary<ARKitBlendShapeLocation, int> faceArkitBlendShapeIndexMap = new Dictionary<ARKitBlendShapeLocation, int>();

    private ARFace face;

    void Awake()
    {
        face = GetComponentInParent<ARFace>();
    }

    private void Start()
    {
        CreateFeatureBlendMapping();
    }

    void CreateFeatureBlendMapping()
    {
        Logger.Instance.LogInfo($"CreateFeatureBlendMapping started");
        if (skinnedMeshRenderer == null || skinnedMeshRenderer?.sharedMesh == null)
        {
            Logger.Instance.LogError("CreateFeatureBlendMapping skinnedMeshRenderer reference is missing");
            return;
        }

        if (blendShapesMapping.mappings == null || blendShapesMapping.mappings.Count == 0)
        {
            Logger.Instance.LogError("Mappings must be configured before using BlendShapeVisualizer");
            return;
        }

        foreach (Mapping mapping in blendShapesMapping.mappings)
        {
            var blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(mapping.name);
            if (blendShapeIndex == -1)
            {
                Logger.Instance.LogWarning($"BlendShape mapping.name: {mapping.name} was not found in mesh");
                continue;
            }
            faceArkitBlendShapeIndexMap.Add(mapping.location, blendShapeIndex);
        }
        Logger.Instance.LogInfo($"CreateFeatureBlendMapping completed mapping blendShapes");
    }

    void SetVisible(bool visible)
    {
        if (skinnedMeshRenderer == null) return;

        skinnedMeshRenderer.enabled = visible;
    }
        
    void UpdateVisibility()
    {
        var visible = enabled && (face.trackingState == TrackingState.Tracking) && (ARSession.state > ARSessionState.Ready);
        SetVisible(visible);
    }

    void OnEnable()
    {
        var faceManager = FindObjectOfType<ARFaceManager>();

        if (faceManager != null)
        {
            arKitFaceSubsystem = (ARKitFaceSubsystem)faceManager.subsystem;
        }

        UpdateVisibility();

        face.updated += OnUpdated;
        ARSession.stateChanged += OnSystemStateChanged;
    }

    void OnDisable()
    {
        face.updated -= OnUpdated;
        ARSession.stateChanged -= OnSystemStateChanged;
    }

    void OnSystemStateChanged(ARSessionStateChangedEventArgs eventArgs)
    {
        UpdateVisibility();
    }

    void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        UpdateVisibility();
        UpdateFaceFeatures();
    }

    void UpdateFaceFeatures()
    {
        if (skinnedMeshRenderer == null || !skinnedMeshRenderer.enabled || skinnedMeshRenderer.sharedMesh == null)
        {
            return;
        }

        using (var blendShapes = arKitFaceSubsystem.GetBlendShapeCoefficients(face.trackableId, Allocator.Temp))
        {
            foreach (var featureCoefficient in blendShapes)
            {
                int mappedBlendShapeIndex;
                if (faceArkitBlendShapeIndexMap.TryGetValue(featureCoefficient.blendShapeLocation, out mappedBlendShapeIndex))
                {
                    if (mappedBlendShapeIndex >= 0)
                    {
                        skinnedMeshRenderer.SetBlendShapeWeight(mappedBlendShapeIndex, featureCoefficient.coefficient * blendShapesMapping.coefficientScale);
                    }
                }
            }
        }
    }
}
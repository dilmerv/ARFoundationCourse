using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARKit;
using System.Collections.Generic;
using Unity.Collections;

[RequireComponent(typeof(ARFace))]
public class ARFaceBlendshapesMapper : MonoBehaviour
{
    [SerializeField]
    private ARFaceBlendshapesMapping blendshapesMapping = null;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer = null;

    private ARKitFaceSubsystem arKitFaceSubsystem;

    private Dictionary<ARKitBlendShapeLocation, int> faceArkitBlendShapeIndexMap = new Dictionary<ARKitBlendShapeLocation, int>();

    private ARFace face;

    void Awake()
    {
        face = GetComponent<ARFace>();
        CreateFeatureBlendMapping();
    }

    void CreateFeatureBlendMapping()
    {
        if (skinnedMeshRenderer == null || skinnedMeshRenderer.sharedMesh == null)
        {
            return;
        }

        if (blendshapesMapping.mappings == null || blendshapesMapping.mappings.Count == 0)
        {
            Debug.LogError("Mappings must be configured before using BlendShapeVisualizer...");
            return;
        }

        foreach (Mapping mapping in blendshapesMapping.mappings)
        {
            faceArkitBlendShapeIndexMap[mapping.location] = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(mapping.name);
        }
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

        if (faceManager != null && faceManager?.subsystem != null)
        {
            arKitFaceSubsystem = (ARKitFaceSubsystem)faceManager?.subsystem;
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
                        skinnedMeshRenderer.SetBlendShapeWeight(mappedBlendShapeIndex, featureCoefficient.coefficient * blendshapesMapping.coefficientScale);
                    }
                }
            }
        }
    }
}
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
#if UNITY_IOS && !UNITY_EDITOR
using UnityEngine.XR.ARKit;
#endif
using UnityEngine.XR.ARSubsystems;

public class ARMeshClassification : MonoBehaviour
{
    [SerializeField]
    private bool enableClassification = true;

    [SerializeField]
    private bool logClassifications = true;

    [SerializeField]
    private GameObject objectToAttachToVertices;

    [SerializeField]
    private LayerMask layersToInclude;

    private InputAction pressAction;

    private ARMeshManager meshManager;

    private void Awake()
    {
        pressAction = new InputAction("touch", binding: "<Pointer>/press");
    }

    void Start()
    {
        meshManager = FindObjectOfType<ARMeshManager>();
#if UNITY_ANDROID
        Logger.Instance.LogWarning("Mesh Classification not supported on android");
#elif UNITY_IOS && !UNITY_EDITOR
        meshManager.meshesChanged += MeshesChanged;
        if(meshManager.subsystem is XRMeshSubsystem meshSubsystem)
        {
            meshSubsystem.SetClassificationEnabled(enableClassification);
        }
#endif
    }

    private void OnEnable()
    {
        pressAction.Enable();
    }

    private void OnDisable()
    {
        pressAction.Disable();
    }

    private void Update()
    {
        var isPressed = pressAction.IsPressed();

        // make sure we're touching the screen and pointer is currently not over UI
        if (!isPressed || EventSystem.current.IsPointerOverGameObject()) return;

        var touchPosition = Pointer.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out var hit, float.PositiveInfinity, layersToInclude))
        {
            Logger.Instance.LogInfo($"Ray Hit at touch position: {touchPosition} w/ hit point: {hit.point}");

            var hitPosition = hit.point;
            Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Instantiate(objectToAttachToVertices, hitPosition, hitRotation);
        }
    }

#if UNITY_IOS && !UNITY_EDITOR
    private void MeshesChanged(ARMeshesChangedEventArgs meshesChangedEventArgs)
    {
        foreach (MeshFilter mesh in meshesChangedEventArgs.updated)
        {
            XRMeshSubsystem meshSubsystem = meshManager.subsystem as XRMeshSubsystem;
            var trackableId = ExtractTrackableId(mesh.name);
            var meshClassifications = meshSubsystem.GetFaceClassifications(trackableId, Allocator.Persistent);

            if (!meshClassifications.IsCreated)
            {
                return;
            }
            using (meshClassifications)
            {
                if(meshClassifications.Length <= 0)
                {
                    return;
                }

                for (int i = 0; i < meshClassifications.Length; i++)
                {
                    if(logClassifications)
                        Logger.Instance.LogInfo(meshClassifications[i].ToString());                    
                }
            }
        }
    }

    private TrackableId ExtractTrackableId(string meshFilterName)
    {
        string[] nameSplit = meshFilterName.Split(' ');
        return new TrackableId(nameSplit[1]);
    }
#endif

}

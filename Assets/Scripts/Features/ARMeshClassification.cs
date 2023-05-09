using Unity.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
#if UNITY_IOS && !UNITY_EDITOR
using UnityEngine.XR.ARKit;
#endif
using UnityEngine.XR.ARSubsystems;

public class ARMeshClassification : MonoBehaviour
{
    private ARMeshManager meshManager;

    // Start is called before the first frame update
    void Start()
    {
        meshManager = FindObjectOfType<ARMeshManager>();
#if UNITY_ANDROID
        Logger.Instance.LogWarning("Mesh Classification not supported on android");
#else
        meshManager.meshesChanged += MeshesChanged;
#endif
    }

    private void MeshesChanged(ARMeshesChangedEventArgs obj)
    {
#if UNITY_IOS && !UNITY_EDITOR
        foreach (MeshFilter mesh in obj.added)
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
                foreach (var classification in meshClassifications)
                {
                    Logger.Instance.LogInfo(classification.ToString());
                }
            }
        }
#endif
    }

    private TrackableId ExtractTrackableId(string meshFilterName)
    {
        string[] nameSplit = meshFilterName.Split(' ');
        return new TrackableId(nameSplit[1]);
    }
}

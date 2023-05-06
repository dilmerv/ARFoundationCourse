using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARFaceManager))]
public class ARFaceManagerInfo : MonoBehaviour
{
    private ARFaceManager faceManager;

    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
    }

    private void OnEnable()
    {
        faceManager.facesChanged += FacesChanged;
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= FacesChanged;
    }

    private void FacesChanged(ARFacesChangedEventArgs facesChangedEventArgs)
    {
        foreach (ARFace face in facesChangedEventArgs.added) 
        {
            Logger.Instance.LogInfo($"Face added with trackableId: {face.trackableId} | vertices: {face.vertices.Count()}");
        }

        foreach (ARFace face in facesChangedEventArgs.removed)
        {
            Logger.Instance.LogInfo($"Face removed with trackableId: {face.trackableId}");
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneDetection : MonoBehaviour
{
    [SerializeField]
    private bool showPlaneTrackableLog = true;

    private ARPlaneManager planeManager;

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        planeManager.planesChanged += PlanesChanged;    
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= PlanesChanged;
    }

    private void PlanesChanged(ARPlanesChangedEventArgs obj)
    {
        DisplayPlanesChanged("Plane Added", obj.added);
        DisplayPlanesChanged("Plane Updated", obj.updated);
        DisplayPlanesChanged("Plane Removed", obj.removed);
    }

    private void DisplayPlanesChanged(string action, IEnumerable<ARPlane> planes)
    {
        if (!showPlaneTrackableLog) return;

        foreach (ARPlane plane in planes)
        {
            Logger.Instance.LogInfo($"{action}: AR Plane trackableId: {plane.trackableId}");
        }
    }
}

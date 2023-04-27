using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCapabilitiesManager : MonoBehaviour
{
    private ARSession arSession;

    private void Awake()
    {
        arSession = FindObjectOfType<ARSession>();
        arSession.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(CheckARSupport());
    }

    private void OnEnable()
    {
        ARSession.stateChanged += ARSessionStateChanged;
    }

    private void OnDisable()
    {
        ARSession.stateChanged -= ARSessionStateChanged;
    }

    private void ARSessionStateChanged(ARSessionStateChangedEventArgs obj)
    {
        Logger.Instance.LogInfo($"AR session state changed: {obj.state}");
    }

    private IEnumerator CheckARSupport()
    {
        if (ARSession.state == ARSessionState.None || ARSession.state == ARSessionState.CheckingAvailability)
        {
            Logger.Instance.LogInfo("Checking if AR is available on this device");
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state != ARSessionState.Unsupported)
        {
            Logger.Instance.LogInfo("AR is supported on this device");
            arSession.enabled = true;
        }
        else
        {
            Logger.Instance.LogInfo("Disabling AR session as it is not supported");
        }
    }
}


using UnityEngine.XR.ARFoundation;
using UnityEngine;

public class ARTrackedImageContentWithLimit : ARTrackedImageContent
{
    [SerializeField]
    private float removeAfterSecondsOfLimitedTracking = 3.0f;

    public ARTrackedImage ARTrackedImage { get; set; }

    private float timeToLive;

    private void Update()
    {
        if (ContentAdded && ARTrackedImage != null && ARTrackedImage.referenceImage.guid != null)
        {
            if (ARTrackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
            {
                if (timeToLive >= removeAfterSecondsOfLimitedTracking)
                {
                    var contentArea = ARTrackedImage.transform.GetChild(1);
                    foreach (Transform content in contentArea.transform)
                    {
                        Destroy(content.gameObject);
                    }
                    ContentAdded = false;
                }
                timeToLive += Time.deltaTime * 1.0f;
            }
            else
            {
                timeToLive = 0;
            }
        }
    }
}

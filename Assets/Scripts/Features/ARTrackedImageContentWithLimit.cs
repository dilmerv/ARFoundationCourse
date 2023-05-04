
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedImageContentWithLimit : ARTrackedImageContent
{
    [SerializeField]
    private float removeAfterSecondsOfLimitedTracking = 3.0f;

    public ARTrackedImage TrackedImage { get; set; }

    public bool EnableThisFeature { get; set; }

    
    private float timeToLive;

    private void Update()
    {
        if (!EnableThisFeature) return;

        if (ContentAdded && TrackedImage != null && TrackedImage.referenceImage.guid != null)
        {
            if (TrackedImage.trackingState == TrackingState.Limited)
            {
                if (timeToLive >= removeAfterSecondsOfLimitedTracking)
                {
                    Logger.Instance.LogInfo("Deleting content on trackable");
                    
                    // hide the UI upon deletion
                    var ui = TrackedImage.transform.GetChild(0);
                    ui.gameObject.SetActive(false);

                    // delete content (3d models, animations, etc)
                    var contentArea = TrackedImage.transform.GetChild(1);
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

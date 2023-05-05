using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedImageContentWithLimit : ARTrackedImageContent
{
    [SerializeField]
    private float removeAfterASecondsOfLimitedTracking = 3.0f;

    public ARTrackedImage TrackedImage { get; set; }

    public bool EnableThisFeautre { get; set; }

    private float timeToLive;

    private void Update()
    {
        if (!EnableThisFeautre) return;

        if (ContentAdded && TrackedImage != null && TrackedImage.referenceImage.guid != null)
        {
            if (TrackedImage.trackingState == TrackingState.Limited)
            {
                if (timeToLive >= removeAfterASecondsOfLimitedTracking)
                {
                    Logger.Instance.LogInfo("Deleting content from trackable");

                    // hide the ui upon deletion
                    var ui = TrackedImage.transform.GetChild(0);
                    ui.gameObject.SetActive(false);

                    // delete content (3d models, animation, etc)
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
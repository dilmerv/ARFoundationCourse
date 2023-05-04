using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARTrackedImageManagerInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToInstantiate;

    [SerializeField]
    private bool allowRemovalUponLimitedTracking = false;

    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void Start()
    {
        Logger.Instance.LogWarning($"Image tracking awake...");

        if (trackedImageManager.subsystem == null)
        {
            Logger.Instance.LogWarning($"Image tracking is NOT supported :(");
            return;
        }

        if (!trackedImageManager.descriptor.supportsImageValidation)
            Logger.Instance.LogWarning($"No supported: supportsImageValidation");
        if (!trackedImageManager.descriptor.supportsMutableLibrary)
            Logger.Instance.LogWarning($"No supported: supportsMutableLibrary");
        if (!trackedImageManager.descriptor.supportsMovingImages)
            Logger.Instance.LogWarning($"No supported: supportsMovingImages");

        Logger.Instance.LogInfo("Starting ARTrackedImageManagerInfo...");
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += TrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= TrackedImagesChanged;
    }

    private void TrackedImagesChanged(ARTrackedImagesChangedEventArgs trackImageEventArgs)
    {
        foreach(var trackedImage in trackImageEventArgs.added)
        {
            Logger.Instance.LogInfo($"New image added: {trackedImage.trackableId}");
            UpdateTrackingInfo(trackedImage);
        }

        foreach (var trackedImage in trackImageEventArgs.updated)
        {
            UpdateTrackingInfo(trackedImage);
        }
    }

    private void UpdateTrackingInfo(ARTrackedImage trackedImage, bool addContent = false)
    {
        // get the UI game object
        var ui = trackedImage.transform.GetComponentInChildren<Canvas>(true);
        ui.gameObject.SetActive(true);

        // get text boxes
        var imageTextBoxes = ui.GetComponentsInChildren<TextMeshProUGUI>();

        if(imageTextBoxes.Length > 0)
        {
            // index = 0 is the image title
            imageTextBoxes[0].text = $"Image Name: {trackedImage.referenceImage.name}";

            // index = 1 is the image description
            imageTextBoxes[1].text = $"Image Information:\n\n Image Id: {trackedImage.referenceImage.guid}\n\nTrackable Id: {trackedImage.trackableId}\n\n " +
                $"Tracking State: {trackedImage.trackingState}\n\n Reference Size: {trackedImage.referenceImage.size} meter(s)\n\n " +
                $"Detected Size Size: {trackedImage.size} meter(s)";
        }

        // get the content game object
        // this is where you can add any 3d object (content)
        var contentArea = trackedImage.transform.GetChild(1);
        if(contentArea != null && objectsToInstantiate?.Length > 0) 
        {
            var imageContent = contentArea.GetComponent<ARTrackedImageContentWithLimit>();
            if(!imageContent.ContentAdded) 
            {
                Instantiate(objectsToInstantiate[Random.Range(0, objectsToInstantiate.Length - 1)],
                    contentArea.transform);
                imageContent.ContentAdded = true;
                imageContent.TrackedImage = trackedImage;
                imageContent.EnableThisFeature = allowRemovalUponLimitedTracking;
            }
        }
    }
}

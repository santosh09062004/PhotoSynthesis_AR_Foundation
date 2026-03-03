using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Modelstay : MonoBehaviour
{
    private ARTrackedImageManager manager;

    // Keep track of currently active image
    private ARTrackedImage currentActiveImage = null;

    void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        manager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        // When a new image is detected
        foreach (var img in args.added)
        {
            ActivateImage(img);
        }

        // If tracking resumes for same image, ignore.
        foreach (var img in args.updated)
        {
            if (img.trackingState == TrackingState.Tracking)
            {
                ActivateImage(img);
            }
        }
    }

    void ActivateImage(ARTrackedImage img)
    {
        if (img.transform.childCount == 0)
            return;

        // Disable previous active model
        if (currentActiveImage != null && currentActiveImage != img)
        {
            if (currentActiveImage.transform.childCount > 0)
            {
                currentActiveImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        // Enable current model
        GameObject model = img.transform.GetChild(0).gameObject;
        model.SetActive(true);

        currentActiveImage = img;
    }
}
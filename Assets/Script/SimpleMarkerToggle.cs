using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SimpleMarkerToggle : MonoBehaviour
{
    private ARTrackedImageManager manager;

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
        foreach (var img in args.added)
            UpdateModel(img);

        foreach (var img in args.updated)
            UpdateModel(img);
    }

    void UpdateModel(ARTrackedImage img)
    {
        if (img.transform.childCount == 0)
            return;

        GameObject model = img.transform.GetChild(0).gameObject;

        model.SetActive(img.trackingState == TrackingState.Tracking);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CalibrateWorld : MonoBehaviour
{
  public GameObject world;

  ARTrackedImageManager m_TrackedImageManager;
  ARSessionOrigin m_SessionOrigin;
  bool firstFrame = true;

  void Start()
  {
    m_SessionOrigin = GetComponent<ARSessionOrigin>();
  }

  void OnEnable()
  {
    m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    m_TrackedImageManager.trackedImagesChanged += OnChanged;
  }

  void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

  // On first frame that image marker is detected, set world position so
  // that the marker is at the center of the world.
  void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
  {
    foreach (var updatedImage in eventArgs.updated)
    {
      if (firstFrame)
      {
        world.transform.position = updatedImage.transform.position;
        firstFrame = false;
      }
    }
  }

  public bool IsCalibrated()
  {
    return firstFrame == false;
  }
}

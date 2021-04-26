using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CalibrateWorld : MonoBehaviour
{
  public GameObject world;
  public GameObject footPrefab;

  ARTrackedImageManager m_TrackedImageManager;
  ARSessionOrigin m_SessionOrigin;

  void Start()
  {
    m_SessionOrigin = GetComponent<ARSessionOrigin>();
  }
  List<GameObject> feet = new List<GameObject>();

  void OnEnable()
  {
    m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
  }

  void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

  // On first frame that image marker is detected, set world position so
  // that the marker is at the center of the world.
  // void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
  // {
  //   foreach (var trackedImage in eventArgs.updated)
  //   {
  //     if (firstFrame)
  //     {
  //       // world.transform.position = updatedImage.transform.position;
  //       m_SessionOrigin.MakeContentAppearAt(world.transform, trackedImage.transform.position, trackedImage.transform.localRotation);
  //       firstFrame = false;
  //     }
  //   }
  // }

  void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
  {
    foreach (var newImage in eventArgs.added)
    {
      if (newImage.referenceImage.name == "foot marker")
      {
        feet.Add(Instantiate(footPrefab, new Vector3(0, 0, 0), Quaternion.identity));
      }
    }

    int feetUpdatedCounter = 0;
    foreach (var updatedImage in eventArgs.updated)
    {
      if (updatedImage.referenceImage.name == "calibration marker")
      {
        m_SessionOrigin.MakeContentAppearAt(world.transform, updatedImage.transform.position, updatedImage.transform.localRotation);
      }
      if (updatedImage.referenceImage.name == "foot marker")
      {
        var foot = feet[feetUpdatedCounter++];
        foot.transform.position = updatedImage.transform.position;
        foot.transform.rotation = updatedImage.transform.rotation;
      }
    }

    foreach (var removedImage in eventArgs.removed)
    {
      Destroy(feet[feet.Count - 1]);
      feet.RemoveAt(feet.Count - 1);
    }
  }
}

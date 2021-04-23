using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScript : MonoBehaviour
{
  public GameObject world;

  private ARTrackedImageManager m_TrackedImageManager;
  private ARSessionOrigin m_SessionOrigin;

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

  bool firstFrame = true;
  void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
  {
    foreach (var newImage in eventArgs.added)
    {

    }

    foreach (var updatedImage in eventArgs.updated)
    {
      if (firstFrame)
      {
        world.transform.position = updatedImage.transform.position;
      }
      firstFrame = false;
    }

    foreach (var removedImage in eventArgs.removed)
    {
      // Handle removed event
    }
  }

  void Update()
  {
    //Debug.Log(m_SessionOrigin.transform.position);
  }
}

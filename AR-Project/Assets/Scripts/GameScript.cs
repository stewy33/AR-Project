using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScript : MonoBehaviour
{
  [SerializeField]
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

  void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
  {
    foreach (var newImage in eventArgs.added)
    {
      Debug.Log(newImage.transform.position);
      m_SessionOrigin.transform.position = newImage.transform.position;
    }

    foreach (var updatedImage in eventArgs.updated)
    {
      // Handle updated event
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

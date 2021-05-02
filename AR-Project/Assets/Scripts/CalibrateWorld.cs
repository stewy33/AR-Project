using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Photon.Pun;

public class CalibrateWorld : MonoBehaviour
{
  public GameObject world;
  public GameObject footPrefab;

  ARTrackedImageManager m_TrackedImageManager;
  ARSessionOrigin m_SessionOrigin;
  private GameObject leftFoot;
  private GameObject rightFoot;

  void Start()
  {
    m_SessionOrigin = GetComponent<ARSessionOrigin>();
  }

  void OnEnable()
  {
    m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
  }

  void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

  // Each time a marker is detected, perform some actions
  void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
  {
    foreach (var updatedImage in eventArgs.updated)
    {
      // Translate AR session origin to world gameobject absolute position + rotation
      if (updatedImage.referenceImage.name == "calibration marker")
      {
        m_SessionOrigin.MakeContentAppearAt(world.transform, updatedImage.transform.position, updatedImage.transform.localRotation);
      }
      // Detect left root and right foot marker, setting respective prefabs' position and rotation
      if (updatedImage.referenceImage.name == "left foot marker")
      {
        if (leftFoot == null)
        {
          leftFoot = PhotonNetwork.Instantiate(footPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
          Debug.LogFormat("Added foot prefab for left foot");
        }
        leftFoot.transform.position = updatedImage.transform.position;
        leftFoot.transform.rotation = updatedImage.transform.rotation;
      }
      if (updatedImage.referenceImage.name == "right foot marker")
      {
        if (rightFoot == null)
        {
          rightFoot = PhotonNetwork.Instantiate(footPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
          Debug.LogFormat("Added foot prefab for right foot");
        }
        rightFoot.transform.position = updatedImage.transform.position;
        rightFoot.transform.rotation = updatedImage.transform.rotation;
      }
    }

    // Remove foot if images are removed
    foreach (var removedImage in eventArgs.removed)
    {
      if (removedImage.referenceImage.name == "left foot marker")
      {
        Debug.LogFormat("Removing left foot image");
        Destroy(leftFoot);
        leftFoot = null;
      }
      if (removedImage.referenceImage.name == "right foot marker")
      {
        Debug.LogFormat("Removing right foot image");
        Destroy(rightFoot);
        rightFoot = null;
      }
    }
  }
}

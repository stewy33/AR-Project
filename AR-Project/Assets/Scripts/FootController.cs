using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FootController : MonoBehaviourPunCallbacks
{
  void Awake()
  {
    Debug.LogFormat("FootController awake called");
    DontDestroyOnLoad(this.gameObject);
  }
}

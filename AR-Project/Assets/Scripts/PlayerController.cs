using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [Tooltip("AR Camera game object")]
    public GameObject arcamera;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    // Track player to its AR camera
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position = arcamera.transform.position;
        }
    }

    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerController.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
        Debug.LogFormat("Assigning gameobject to local player");
    }
}

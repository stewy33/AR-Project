using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [Tooltip("AR Camera game object")]
    public GameObject arcamera;
    [Tooltip("World game object")]
    public GameObject world;
    public GameObject arSessionOrigin;

    private bool freshInstance;
    private Vector3 offset;
    // private WorldController worldscript;
    private CalibrateWorld arSessionOriginScript;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, 0);
        freshInstance = true;
        if (photonView.IsMine)
        {
            arSessionOriginScript = arSessionOrigin.GetComponent<CalibrateWorld>();
        }
    }

    // Track player to its AR camera
    void Update()
    {
        if (photonView.IsMine)
        {
            Debug.LogFormat("AR Camera Position {0}", arcamera.transform.position);
            Debug.LogFormat("World Position {0}", world.transform.position);
            if (freshInstance && arSessionOriginScript.IsCalibrated())
            {
                offset = world.transform.position - arcamera.transform.position;
                Debug.LogFormat("Transform has changed, offset: {0}", offset);
                freshInstance = false;
            }
            if (!offset.Equals(Vector3.zero))
            {
                transform.position = arcamera.transform.position + offset;
            }   
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

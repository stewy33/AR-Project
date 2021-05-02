using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SoccerBallController : MonoBehaviourPunCallbacks, IPunObservable
{
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        networkPosition = rb.position;
        networkRotation = rb.rotation;
    }

    // Explicit callback to synchronize soccer ball (accounts for lag)
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Master may write to the rest of the clients
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else
        {
            networkPosition = (Vector3) stream.ReceiveNext();
            networkRotation = (Quaternion) stream.ReceiveNext();
            rb.velocity = (Vector3) stream.ReceiveNext();

            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
            networkPosition += rb.velocity * lag;
        }
    }

    // Move rigidbody based on the new values
    void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            rb.MovePosition(networkPosition);
            rb.MoveRotation(networkRotation);
        }
    }
}

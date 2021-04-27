using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Debug.LogFormat("stream is writing");
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
        }
        else
        {
            // Debug.LogFormat("stream is reading");
            networkPosition = (Vector3) stream.ReceiveNext();
            networkRotation = (Quaternion) stream.ReceiveNext();
            rb.velocity = (Vector3) stream.ReceiveNext();

            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
            networkPosition += rb.velocity * lag;
        }
    }

    void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            rb.MovePosition(networkPosition);
            rb.MoveRotation(networkRotation);
        }
    }

    // [PunRPC]
    // public void UpdateMasterSoccerBall(Vector3 pos, Quaternion rot, Vector3 vel, PhotonMessageInfo info)
    // {
    //     Debug.LogFormat("Update Master Soccer Ball got called");
    //     if (PhotonNetwork.IsMasterClient)
    //     {
    //         Debug.LogFormat("And we're the master (should always be true)");
    //         var rb = GetComponent<Rigidbody>();
    //         Debug.LogFormat("Info: {0} {1} {2}", info.Sender, info.photonView, info.SentServerTime);
    //         float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
    //         pos += vel * lag;
    //         // rb.MovePosition(pos);
    //         // rb.MoveRotation(rot);
    //         rb.position = pos;
    //         rb.rotation = rot;
    //         rb.velocity = vel;
    //         return;
    //     }
    // }

    // void OnCollisionEnter(Collision col)
    // {
    //     Debug.LogFormat("Collision detected on soccer ball");
    //     if (col.gameObject.name.Contains("Foot") && col.gameObject.GetComponent<PhotonView>().IsMine)
    //     {
    //         Debug.LogFormat("Our collision, and we're not the master");
    //         var rb = GetComponent<Rigidbody>();
    //         photonView.RPC("UpdateMasterSoccerBall", RpcTarget.MasterClient, rb.position, rb.rotation, rb.velocity);
    //     }
    // }
}

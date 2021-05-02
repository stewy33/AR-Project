using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public GameObject world;
    public GameObject arcamera;

    #region Photon Callbacks
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("player prefab is not set", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer");
            // spawn character for local player - gets synced by using PhotonNetwork.Instantiate
            if (PlayerController.LocalPlayerInstance == null)
            {
                var newPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
                newPlayer.transform.parent = world.transform;
                var pc = newPlayer.GetComponent<PlayerController>();
                pc.arcamera = arcamera;
            }
            else
            {
                Debug.LogFormat("Ignoring scene load");
            }
        }
    }

    // Called when the local player left the room - load launcher scene
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Private Methods

    // Load ARRoom level
    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("ARRoom");
    }


    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);


            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);


            LoadArena();
        }
    }


    #endregion
}
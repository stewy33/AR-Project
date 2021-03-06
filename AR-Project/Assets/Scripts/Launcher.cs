using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
  #region Private Serializable Fields
  [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
  [SerializeField]
  private byte maxPlayersPerRoom = 5;

  [Tooltip("The Ui Panel to let the user enter name, connect and play")]
  [SerializeField]
  private GameObject controlPanel;

  [Tooltip("The UI Label to inform the user that the connection is in progress")]
  [SerializeField]
  private GameObject progressLabel;
  #endregion

  #region Private Fields
  string gameVersion = "1";
  bool isConnecting;
  #endregion

  #region MonoBehaviour CallBacks
  void Awake()
  {
    PhotonNetwork.AutomaticallySyncScene = true;
    Screen.orientation = ScreenOrientation.Portrait;
  }

  void Start()
  {
    progressLabel.SetActive(false);
    controlPanel.SetActive(true);
  }
  #endregion

  #region Public Methods
  public void Connect()
  {
    progressLabel.SetActive(true);
    controlPanel.SetActive(false);
    if (PhotonNetwork.IsConnected)
    {
      PhotonNetwork.JoinRandomRoom();
    }
    else
    {
      isConnecting = PhotonNetwork.ConnectUsingSettings();
      PhotonNetwork.GameVersion = gameVersion;
    }
  }
  #endregion

  #region MonBehaviourPunCallbacks Callbacks
  public override void OnConnectedToMaster()
  {
    Debug.Log("OnConnectedToMaster was called by PUN tutorial");
    if (isConnecting)
    {
      // Attempt to join random room
      PhotonNetwork.JoinRandomRoom();
      isConnecting = false;
    }
  }

  public override void OnDisconnected(DisconnectCause cause)
  {
    Debug.LogWarningFormat("OnDisconnected was called by PUN tutorial");
    progressLabel.SetActive(false);
    controlPanel.SetActive(true);
    isConnecting = false;
  }

  public override void OnJoinRandomFailed(short returnCode, string message)
  {
    // May not be a current room available, create a new room
    Debug.Log("OnJoinRandomFailed was called by PUN");
    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
  }

  public override void OnJoinedRoom()
  {
    Debug.Log("OnJoinedRoom was called by PUN tutorial");
    // Load room if first player
    if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
    {
      Debug.Log("We load the 'Room' ");


      // Load the Room Level
      PhotonNetwork.LoadLevel("ARRoom");
    }
  }
  #endregion
}

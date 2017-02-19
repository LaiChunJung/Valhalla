using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeServer : Photon.MonoBehaviour {

	public string verNum = "0.2";
    public string roomName = "room01";
    public string playerName = "Player01";
    public Transform spawnPoint;
    public GameObject playerPref;
    public bool isConnected = false;

    void Start()
    {
        roomName = "Room" + Random.Range(0, 999);
        playerName = "Player" + Random.Range(0, 999);
        PhotonNetwork.ConnectUsingSettings(verNum); 
        Debug.Log("Starting Connection!");
    }

    public void OnJoinedLobby()
    {
        //PhotonNetwork.JoinOrCreateRoom(roomName, null, null); //joinroom people condition limit
        isConnected = true;
        Debug.Log("Starting Server!");
    }

    public void OnJoinedRoom()
    {
        PhotonNetwork.playerName = playerName;
        isConnected = false;
        spawnPlayer();
    }

    public void spawnPlayer()
    {
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        //Debug.Log("(‧‧)nnn~ (‧‧)nnn~ (‧‧)nnn~");
        Manager.Instance.PlayerJoin = true;
        CameraCtrl.Instance.target = pl.transform;
        Player.Instance.SetFalling();
        //pl.GetComponent<RigidbodyPlayer>().enabled = true;
        //pl.GetComponent<RigidbodyPlayer>().fpsCam.SetActive(true);
    }

    void OnGUI()
    {
        if (isConnected)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));
            playerName = GUILayout.TextField(playerName);
            roomName = GUILayout.TextField(roomName);

            if (GUILayout.Button("Create"))
            {
                PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
            }
            Debug.Log("（°ο°）~ @　" + PhotonNetwork.GetRoomList());

            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                Debug.Log("（°ο°）~ @　");
                if (GUILayout.Button(game.Name + " " + game.PlayerCount + "/" + game.MaxPlayers))
                {
                    PhotonNetwork.JoinOrCreateRoom(game.Name, null, null);
                }
            }
            GUILayout.EndArea();
        }
    }
}

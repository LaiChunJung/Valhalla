using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobby : Photon.MonoBehaviour
{
    public GameRoom MGameRoom = null;

	public string verNum = "0.2";
    public bool MisConnected = false;
    public bool MisInRoom = false;
    public static bool MisJoin = false;
    /*
    public Transform spawnPoint;
	public GameObject playerPref;
	public GameObject player2;
    */

	void Start()
	{
        MisConnected = false;
        MisInRoom = false;
        MisJoin = false;

        PhotonNetwork.ConnectUsingSettings(verNum);
		Debug.Log("Starting Connection!");
	}

    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.E))
        {
            spawnPlayer();
        }*/
    }

	public void OnJoinedLobby()
	{
        //PhotonNetwork.JoinOrCreateRoom(roomName, null, null); //joinroom people condition limit
        MisConnected = true;
        MisJoin = true;
        Debug.Log("Starting Server!");

    }
    
	public void OnJoinedRoom()
	{
        MGameRoom.LobbyRoomName.text = PhotonNetwork.room.Name;

        int ATeamNum = 0;
        int BTeamNum = 0;

        PhotonNetwork.playerName = MGameRoom.IN_PlayerName.text;
        MisConnected = false;
        MisInRoom = true;

        foreach(PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.GetTeam() == PunTeams.Team.red) { ATeamNum += 1; }
            else if (player.GetTeam() == PunTeams.Team.blue) { BTeamNum += 1; }
        }
        if (ATeamNum >= BTeamNum) { PhotonNetwork.player.SetTeam(PunTeams.Team.blue); }
        else if (ATeamNum < BTeamNum) { PhotonNetwork.player.SetTeam(PunTeams.Team.red); }

        if (!PhotonNetwork.player.IsMasterClient)
        {
            MGameRoom.BtnReady.SetActive(PhotonNetwork.player.IsLocal);
        }
        else
            MGameRoom.BtnStart.SetActive(PhotonNetwork.player.IsMasterClient);

        //spawnPlayer();
        Debug.Log("Join ~Room!");
    }
    /*
	public void spawnPlayer()
	{
        //Debug.Log("<(￣︶￣)/ " + PhotonNetwork.playerList.Length);
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        Player p1script = pl.GetComponent<Player>() as Player;
        p1script.enabled = true;
        FakerServer.Instance.AddPlayerlist(p1script);
        //Debug.Log("(‧‧)nnn~ (‧‧)nnn~ (‧‧)nnn~");
        Manager.Instance.PlayerJoin = true;
        CameraCtrl.Instance.target = pl.transform;
    }

	void OnGUI()
	{
		if (MisConnected)
		{
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));
			playerName = GUILayout.TextField(playerName);
			roomName = GUILayout.TextField(roomName);

			if (GUILayout.Button("Create"))
			{
				PhotonNetwork.JoinOrCreateRoom(roomName, null, null);

			}

			foreach (RoomInfo game in PhotonNetwork.GetRoomList())
			{
				if (GUILayout.Button(game.Name + " " + game.PlayerCount + "/" + game.MaxPlayers))
				{
					PhotonNetwork.JoinOrCreateRoom(game.Name, null, null);
				}
			}
			GUILayout.EndArea();
		}
	}*/
}

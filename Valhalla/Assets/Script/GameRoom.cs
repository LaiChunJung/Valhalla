using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoom : MonoBehaviour {

    public string roomName = "room01";
    public string playerName = "Player01";

    private string CurrentGameScene = "";

    private byte MaxPlayer = 4;
    public Text LobbyRoomName = null;
    public InputField IN_RoomName = null;
    public InputField IN_PlayerName = null;

    public GameObject Obj_CreatRoom = null;
    public GameObject Obj_GameLobby = null;
    public GameObject BtnStart = null;
    public GameObject BtnReady = null;

    private RoomInfo[] GameRoomInfoArray;
    public GameObject[] BtnsJoinRoom;

    // Use this for initialization
    void Start () {
        roomName = "Room" + Random.Range(0, 999);
        playerName = "Player" + Random.Range(0, 999);
        IN_RoomName.text = roomName;
        IN_PlayerName.text = playerName;

        Obj_CreatRoom.SetActive(true);
        Obj_GameLobby.SetActive(false);

        CurrentGameScene = "Scene";
    }

    // Update is called once per frame
    void Update () {
		
	}
    /// <summary>
    /// Start Game Button
    /// </summary>
    public void StartGameButton()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            int ATeamMemberNum = 0;
            int BTeamMemberNum = 0;
            bool AllReady = true;
            /*
            foreach (PhotonPlayer Player in PhotonNetwork.playerList)
            {

                var CustomProperties = Player.CustomProperties;
                string PlayerReadyProperties = GameLobbySystem.GetPlayerReadyProperties();
                if ((bool)CustomProperties[PlayerReadyProperties] != true)
                {
                    AllReady = false;
                }
                switch (Player.GetTeam())
                {
                    case PunTeams.Team.blue: { BTeamMemberNum += 1; } break;
                    case PunTeams.Team.red: { ATeamMemberNum += 1; } break;
                }
                Debug.Log(Player.NickName + "  |  " + (bool)CustomProperties[PlayerReadyProperties]);
            }
            if (PhotonNetwork.playerList.Length != 1)
            {
                if (!AllReady || ATeamMemberNum == 0 || BTeamMemberNum == 0) { return; };
            }*/
            PhotonNetwork.room.open = false;
            PhotonNetwork.room.visible = false;
            GetComponent<PhotonView>().RPC("LoadGameSence", PhotonTargets.All, null);
        }
    }

    /// <summary>
    /// Load Game Stage
    /// </summary>
    [PunRPC]
    public void LoadGameSence()
    {
        PhotonNetwork.LoadLevel(CurrentGameScene);
    }

    /// <summary>
    /// Creat Room Button
    /// </summary>
    public void CreatRoomButoon()
    {
        //要多判斷OnJoinedLobby()已執行 不然會有錯誤
        if (!GameLobby.MisJoin) return;

        PhotonNetwork.JoinOrCreateRoom(IN_RoomName.text, new RoomOptions() { maxPlayers = MaxPlayer }, null);
        Obj_CreatRoom.SetActive(false);
        Obj_GameLobby.SetActive(true);
    }

    public void JoinRoom(int Num)
    {
        playerName = IN_PlayerName.text;
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.JoinOrCreateRoom(GameRoomInfoArray[Num].Name, null, null);
    }

    /// <summary>
    ///  Room State Update
    /// </summary>
    private void RoomStateUpdate()
    {
        GameRoomInfoArray = PhotonNetwork.GetRoomList();
        foreach (GameObject JointButton in BtnsJoinRoom)
        {
            if (JointButton.activeInHierarchy)
            {
                JointButton.SetActive(false);
            }
        }
        int count = 0;
        for (int i = 0; i < GameRoomInfoArray.Length; i++)
        {
            count++;
            string GameRoomExplain = GameRoomInfoArray[i].Name + " | CurrentPlayer: " + GameRoomInfoArray[i].PlayerCount + " | MaxPlayer: " + GameRoomInfoArray[i].MaxPlayers;
            BtnsJoinRoom[i].gameObject.SetActive(true);
            BtnsJoinRoom[i].GetComponentInChildren<Text>().text = GameRoomExplain;
        }
        //RoomListAnimator.SetInteger("RoomListCount", count);
    }
}



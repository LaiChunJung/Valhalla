using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakerServer : MonoBehaviour
{
    private static FakerServer _instance = null;
    public static FakerServer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FakerServer>();

                if (_instance == null)
                {
                    GameObject manager = new GameObject("FakerServer");
                    _instance = manager.AddComponent<FakerServer>();
                }
            }
            return _instance;
        }
    }

    private List<Player> SPlayerList = new List<Player>();

    public string GetPlayerlist(int Playerindex)
    {
        return SPlayerList[Playerindex].name;
    }

    public void AddPlayerlist(Player PlayerScript)
    {
        if (SPlayerList == null)
        {
            Debug.Log("~~~〒▽〒~~~");
            return;
        }
        SPlayerList.Add(PlayerScript);
        Debug.Log("~~~~~~<(￣︶￣)/ / / " + SPlayerList.Count);
    }

    public void RemovePlayerList()
    {

    }
}

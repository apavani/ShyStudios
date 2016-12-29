using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour {

    public GameObject[] playerPrefabs;
    private GameObject newPlayer;

    private Dictionary<string,GameObject> playerObjectDictionary = new Dictionary<string,GameObject>();
	// Use this for initialization
	void OnEnable () {
        MultiplayerListenerClass.Instance.PlayerEvents += HandlePlayerData;
        MultiplayerListenerClass.Instance.RoomConnected += InstantiateSelf;
	}

    private void InstantiateSelf(object sender, System.EventArgs e)
    {
        GameObject go = Instantiate(playerPrefabs[0],Vector3.zero,Quaternion.identity);
        go.GetComponent<CharacterMover>().enabled = true;
    }

    private void HandlePlayerData(object sender, PlayerData p)
    {
        if(!playerObjectDictionary.ContainsKey(p.senderID))
        {
            newPlayer = (GameObject)Instantiate(playerPrefabs[0], Vector3.zero, Quaternion.identity);
            playerObjectDictionary.Add(p.senderID, newPlayer);
            return;
        }

            playerObjectDictionary.TryGetValue(p.senderID, out newPlayer);
        

        newPlayer.GetComponent<PeerCharacterManager>().ProcessMessage(p.data);
    }

    void OnDisable()
    {
        MultiplayerListenerClass.Instance.PlayerEvents -= HandlePlayerData;
        MultiplayerListenerClass.Instance.RoomConnected -= InstantiateSelf;
    }
}

using UnityEngine;
using System;
using GooglePlayGames.BasicApi.Multiplayer;

public class MultiplayerListenerClass : RealTimeMultiplayerListener {

    private static MultiplayerListenerClass _instance;
    public static MultiplayerListenerClass Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MultiplayerListenerClass();
            return _instance;
        }
    }
    public event EventHandler<PlayerData> PlayerEvents;
    public event EventHandler<EventArgs> RoomConnected;

    private PlayerData _playerData = new PlayerData();
    public void OnLeftRoom()
    {
        Debug.Log("Room Left");
    }

    public void OnParticipantLeft(Participant participant)
    {
        Debug.Log("Participant Left :"+ participant.DisplayName);
    }

    public void OnPeersConnected(string[] participantIds)
    {
        Debug.Log("Participant connected count = "+participantIds.Length);
    }

    public void OnPeersDisconnected(string[] participantIds)
    {
        Debug.Log("Participant disconnected count = " + participantIds.Length);
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        _playerData.isReliable = isReliable;
        _playerData.senderID = senderId;
        _playerData.data = data;

        if (PlayerEvents != null)
            PlayerEvents(this, _playerData);
    }

    public void OnRoomConnected(bool success)
    {
        Debug.Log("Room Connected");
        //Instantiate player
        if (RoomConnected != null)
            RoomConnected(this, EventArgs.Empty);
    }

    public void OnRoomSetupProgress(float percent)
    {
        if (percent <= 20)
        {
            Debug.Log("Waiting for the other player to connect");
        }
        else {
            Debug.Log("Room Setup in progress: " + percent + "%");
        }
    }

}

public class PlayerData:EventArgs
{
    public bool isReliable { get; set; }
    public string senderID { get; set; }
    public byte[] data { get; set; }
}

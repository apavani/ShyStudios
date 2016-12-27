using UnityEngine;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames;

public class MultiplayerListenerClass : RealTimeMultiplayerListener {
    private bool showingWaitingRoom = false;

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
        Debug.Log("Message Received from sender "+senderId+" : " + System.Text.Encoding.Default.GetString(data));
    }

    public void OnRoomConnected(bool success)
    {
        Debug.Log("Room Connected");
    }

    public void OnRoomSetupProgress(float percent)
    {
        if (!showingWaitingRoom)
        {
            showingWaitingRoom = true;
            PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
        }
    }

}

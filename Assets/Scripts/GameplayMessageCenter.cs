using System;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GameplayMessageCenter {
    /*
    * P = Position
    * G = Grab
    * D = Drop
    * C = Collect
    */
    List<byte> positionMessage = new List<byte>(9);
    byte[] grabMessage = new byte[2];
    byte[] collectMessage = new byte[2] ;

    List<byte> dropMessage = new List<byte>(10); 

    private static GameplayMessageCenter _instance;

    public static GameplayMessageCenter Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameplayMessageCenter();
            return _instance;
        }
    }

    public void SendPlayerPositionData(Vector2 positionVector)
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
            return;
        if (PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants().Count < 2)
            return;
            //message size will be: message type(1 byte) + Vector2 Position (8 bytes)
        positionMessage.Clear();
        positionMessage.Add((byte)'P'); // P stands for Position Type message
        positionMessage.AddRange(BitConverter.GetBytes(positionVector.x));
        positionMessage.AddRange(BitConverter.GetBytes(positionVector.y));
        byte[] messageToSend = positionMessage.ToArray();
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, messageToSend); //sending unreliable message
    }

    public void ReceivePlayerPositionData(byte[] data)
    {

    }

    public void SendGrab(byte gemID)
    {
        grabMessage[0]=((byte)'G'); // G stands for Grab Type message
        grabMessage[1]=gemID;//GemID
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, grabMessage); //sending reliable message
    }

    public void SendDrop(byte gemID, float dropLocationX, float dropLocationY)
    {
        dropMessage.Clear();
        dropMessage.Add((byte)'D'); // D stands for Drop Type message
        dropMessage.Add(gemID);
        dropMessage.AddRange(BitConverter.GetBytes(dropLocationX));
        dropMessage.AddRange(BitConverter.GetBytes(dropLocationY));
        byte[] messageToSend = dropMessage.ToArray();
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, messageToSend); //sending reliable message
    }

    public void SendCollect(byte gemID)
    {
        collectMessage[0] = ((byte)'C'); // C stands for Collect Type message
        collectMessage[1] = gemID;//GemID
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, collectMessage); //sending reliable message
    }

}

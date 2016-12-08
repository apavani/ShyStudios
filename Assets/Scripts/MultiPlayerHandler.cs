using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;

public class MultiPlayerHandler : MonoBehaviour {

    MultiplayerListenerClass listener;
    // Use this for initialization
    void Start () {
        listener = new MultiplayerListenerClass();
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(0, 5,
               0, listener);
    }

}

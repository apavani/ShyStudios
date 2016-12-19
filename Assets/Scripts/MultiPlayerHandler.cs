using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using UnityEngine.UI;

public class MultiPlayerHandler : MonoBehaviour {

    MultiplayerListenerClass listener;
    // Use this for initialization
    public void CreateMultiplayerGame () {
        listener = new MultiplayerListenerClass();
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(1, 4,
               0, listener);
        GetComponent<Button>().enabled = false;
    }

}

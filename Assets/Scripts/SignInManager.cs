using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class SignInManager : MonoBehaviour {
    // authenticate user:
    void Start()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("Login Successful");
            // handle success or failure
        });
    }
}

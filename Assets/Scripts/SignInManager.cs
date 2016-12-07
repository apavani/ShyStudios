using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class SignInManager : MonoBehaviour {
    // authenticate user:
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
            //standby login screen to be implemented while GPGS is signing in
            if (success)
                {
                    Debug.Log("Login Successful");
                    string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                    Debug.Log(userInfo);
                }
                else
                    Debug.Log("Login Failure");
            // handle success or failure
            });
        }
    }
    
}

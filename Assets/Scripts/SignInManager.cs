using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;

public class SignInManager : MonoBehaviour {
    // authenticate user:
    void OnEnable()
    {
        InvitationReceivedDelegate invitationWhenClosed = ReceiveInvitationWhenAppClosed;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       // enables saving game progress.
       //.EnableSavedGames()
       // registers a callback to handle game invitations received while the game is not running.
       .WithInvitationDelegate(invitationWhenClosed)
       // require access to a player's Google+ social graph (usually not needed)
       //.RequireGooglePlus()
       .Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        StartCoroutine("SignInToGPGS");
    }

    private void ReceiveInvitationWhenAppClosed(Invitation invitation, bool shouldAutoAccept)
    {

    }

    private IEnumerator SignInToGPGS()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("User Already Authenticated = " + PlayGamesPlatform.Instance.localUser.authenticated);
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
            {
                //standby login screen to be implemented while GPGS is signing in
                if (success)
                {
                    Debug.Log("Login Successful");
                    string userInfo = "Username: " + PlayGamesPlatform.Instance.localUser.userName +
                    "\nUser ID: " + PlayGamesPlatform.Instance.localUser.id +
                    "\nIsUnderage: " + PlayGamesPlatform.Instance.localUser.underage;
                    Debug.Log(userInfo);

                    MultiPlayerHandler.Instance.CreateMultiplayerGame();
                    
                }
                else
                    Debug.Log("Login Failed");
                // handle success or failure
            });
        }
    }

    void OnDisable()
    {

    }
}

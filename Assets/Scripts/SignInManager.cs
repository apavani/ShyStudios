using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;

public class SignInManager : MonoBehaviour {
    // authenticate user:
    void Start()
    {
        InvitationReceivedDelegate invitationWhenClosed = ReceiveInvitationWhenAppClosed;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       // enables saving game progress.
       //.EnableSavedGames()
       // registers a callback to handle game invitations received while the game is not running.
       .WithInvitationDelegate(invitationWhenClosed)
       // require access to a player's Google+ social graph (usually not needed)
       //.RequireGooglePlus()
       // GPGS built
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
                    string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                    Debug.Log(userInfo);
                }
                else
                    Debug.Log("Login Failed");
                // handle success or failure
            });
        }
    }
}

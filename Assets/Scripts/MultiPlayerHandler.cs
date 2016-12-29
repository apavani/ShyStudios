using GooglePlayGames;


public class MultiPlayerHandler  {

    private static MultiPlayerHandler _instance;
    public static MultiPlayerHandler Instance {
        get{
            if(_instance==null)
            {
                _instance = new MultiPlayerHandler();
            }
            return _instance;
        }
    }

    // Use this for initialization
    public void CreateMultiplayerGame () {
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(1, 4, 0, MultiplayerListenerClass.Instance);
    }

}

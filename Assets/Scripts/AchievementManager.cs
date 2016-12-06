using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;

public class AchievementManager : MonoBehaviour {

	public void Achievement1Complete()
    {
        Social.ReportProgress("CgkI0cD_460OEAIQAg", 100.0,(bool success) => {
            if (success)
            {
                transform.Find("Text").GetComponent<Text>().text = "#1 Completed!";
                gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                Debug.LogError("failed to do it");
            }
        });
    }
}

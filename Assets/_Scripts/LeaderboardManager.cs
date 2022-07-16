using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    public static string playerID;
    private static int _leaderboardID = 4760;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoginRoutine());
    }

    private IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                LeaderboardManager.playerID = response.player_id.ToString();
                Debug.Log("Player was logged in with playerID " + playerID);
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitUntil(() => done);
    }

    public void SubmitScore(int scoreToUpload)
    {
        StartCoroutine(SubmitScoreRoutine(scoreToUpload));
    }

    public void SetName(string playerName)
    {
        StartCoroutine(SetNameRoutine(playerName));
    }

    private IEnumerator SetNameRoutine(string playerName)
    {
        bool done = false;
        LootLockerSDKManager.SetPlayerName(playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully changed name to " + playerName);
                done = true;
            }
            else
            {
                Debug.Log("Failed to change name");
                done = true;
            }
        });
        yield return new WaitUntil(() => done);
    }

    private IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, _leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score: " + scoreToUpload);
                done = true;
            }
            else
            {
                Debug.Log("Failed to upload score");
                done = true;
            }
        });
        yield return new WaitUntil(() => done);
    }
    
}

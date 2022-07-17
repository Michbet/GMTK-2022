using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LootLocker.Requests;
using LootLocker.Admin;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    public static string PlayerID;
    public static LootLockerLeaderboardMember[] Scores;
    private static int _leaderboardID => 4760;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        StartCoroutine(LoginRoutine());
    }

    private IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                LeaderboardManager.PlayerID = response.player_id.ToString();
                Debug.Log("Player was logged in with playerID " + PlayerID);
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

    public void SetNameAndScore(string username, int score)
    {
        StartCoroutine(SetNameAndScoreRoutine(username, score));
    }

    public IEnumerator SetNameAndScoreRoutine(string username, int score)
    {
        yield return LoadScoresRoutine();
        yield return SetNameRoutine(username);
        yield return SubmitScoreRoutine(score);
    }

    private LootLockerLeaderboardMember GetMemberFromName(string playerName)
    {
        if (Scores == null)
            return null;
        foreach (var score in Scores)
        {
            if (score.player.name == playerName)
                return score;
        }
        return null;
    }

    public IEnumerator SetNameRoutine(string playerName)
    {
        var member = GetMemberFromName(playerName);
        if (member != null)
        {
            Debug.Log("Found member: " + member.player.id + " with " + member.member_id);
            PlayerID = member.member_id;   
        }
        
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

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        LootLockerSDKManager.SubmitScore(PlayerID, scoreToUpload, _leaderboardID, (response) =>
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

    public void LoadScores()
    {
        StartCoroutine(LoadScoresRoutine());
    }

    public IEnumerator LoadScoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(_leaderboardID, 100, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("retrieved top 100 scores");
                Scores = response.items;
            }
            else
            {
                Debug.Log("couldn't retrive scores");
            }

            done = true;
        });
        yield return new WaitUntil(() => done);
    }

}

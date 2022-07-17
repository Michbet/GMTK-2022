using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_InputField nameInput;
    public Button submitButton;
    public GameObject leaderboardUI;

    public void OnSubmit()
    {
        if (nameInput.text == "")
            return;
        LeaderboardManager.Instance.SetNameAndScore(nameInput.text, GameManager.level);
        submitButton.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
    }

    private void Start()
    {
        levelText.text = "You made it to level "+GameManager.level.ToString();
    }

    public void ShowScores()
    {
        gameObject.SetActive(false);
        leaderboardUI.SetActive(true);
    }

    public static void OnPlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

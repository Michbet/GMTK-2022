using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void playGame()
    {
        SFXManager.Play("Button Push");
        SceneManager.LoadScene("SampleScene");
    }
}

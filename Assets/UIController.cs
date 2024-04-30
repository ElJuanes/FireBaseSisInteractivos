using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Game UI")]
   
    public TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameUI;

    [Header("Menu")]
    [SerializeField] GameObject menuUI;

    [Header("Scoreboard")]
    [SerializeField] GameObject scoreboardUI;

    [Header("SCore")]
    [SerializeField] GameObject ScoreUsserUI;

    [SerializeField] ScoreboardManager scoreboardManager;
    /* private void Start()
     {
         scoreManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreBoardManager>();
     }*/

    /*public void UpdateScore()
    {
        scoreText.text = scoreManager.score.ToString();
    }*/

    public void PlayButton()
    {
        gameUI.SetActive(true);
        menuUI.SetActive(false);       
       
    }

    public void ShowScoreboard()
    {
        scoreboardUI.SetActive(true);
        menuUI.SetActive(false);
        ScoreUsserUI.SetActive(false);

       // scoreboardManager.GetScoreboardData();

    }

    public void BackToMenu()
    {
        menuUI.SetActive(true);
        scoreboardUI.SetActive(false);
        ScoreUsserUI.SetActive(true);
        gameUI.SetActive(false);
    }

   

}

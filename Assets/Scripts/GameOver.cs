using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {

    public Text Score;
    public Text HighScore;
	// Use this for initialization
	void Start () {
        Score.text = ""+PlayerPrefs.GetInt("score");
        HighScore.text = "" + PlayerPrefs.GetInt("highscore");
        BallControl.score = 0;
	}
	
	public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void LeaderBoard()
    {

    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShareFb()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }

}

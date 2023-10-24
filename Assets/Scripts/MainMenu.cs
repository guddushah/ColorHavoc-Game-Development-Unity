using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
	}
	
	public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void LeaderBoard()
    {

    }

    public void Rate()
    {

    }

    public void ShareFb()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipScript : MonoBehaviour {
    public GameObject Tips;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowGuide()
    {
        if (PlayerPrefs.GetInt("EnterRage") == 0)
        {
            Tips.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void HideGuide()
    {
        Tips.SetActive(true);
        PlayerPrefs.SetInt("EnterRage", 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    public static int songScore, songStop = 3;
    [SerializeField] Text songScoreLabel, songStopLabel;
    [SerializeField] AdsManager adsManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        songScoreLabel.text = songScore.ToString();
        songStopLabel.text = songStop.ToString();

        if(songStop <= 0)
        {
            StartGame.startGame = false;
            adsManager.ShowAds();
            songStop = 3;
            songScore = 0;
        }

        if(StartGame.startGame == false)
        {
            songStop = 3;
            songScore = 0;
        }
	}
}

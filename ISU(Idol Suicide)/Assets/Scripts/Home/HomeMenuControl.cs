using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMenuControl : MonoBehaviour {

    [SerializeField] GameObject homeUI;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(StartGame.startGame == false)
        {
            InitHome();
        }
	}

    public void InitGame()
    {
        homeUI.SetActive(false);
    }

    public void InitHome()
    {
        homeUI.SetActive(true);
    }
}

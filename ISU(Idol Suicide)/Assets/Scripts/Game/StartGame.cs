using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public static bool startGame = false;
    [SerializeField] VisualizerExample visualizerExample;
    [SerializeField] MoveObstacle moveObstacle1, moveObstacle2, moveObstacle3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void PlayGame()
    {
        startGame = true;
        visualizerExample.NextSong();
        moveObstacle1.RestartPosition();
        moveObstacle2.RestartPosition();
        moveObstacle3.RestartPosition();
    }
}

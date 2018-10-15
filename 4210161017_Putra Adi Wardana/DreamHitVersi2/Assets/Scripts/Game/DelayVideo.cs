using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DelayVideo : MonoBehaviour {

    [SerializeField] VideoPlayer onlineVideo;
    float timerPlay = 10;

	// Use this for initialization
	void Start () {
        onlineVideo.Pause();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

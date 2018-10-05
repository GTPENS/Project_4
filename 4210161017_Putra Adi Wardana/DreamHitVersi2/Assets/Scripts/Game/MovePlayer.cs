using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] Transform rightTarget, leftTarget, midTarget;
    [SerializeField] GameObject player;


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.acceleration.x;
        float step = speed * Time.deltaTime;
        if (x < -0.1f)
            player.transform.position = Vector3.MoveTowards(transform.position, leftTarget.position, step);
        else if(x > 0.1f)
            player.transform.position = Vector3.MoveTowards(transform.position, rightTarget.position, step);
        else
            player.transform.position = Vector3.MoveTowards(transform.position, midTarget.position, step);
    }
}

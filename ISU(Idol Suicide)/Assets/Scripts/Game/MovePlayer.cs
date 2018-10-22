using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    [SerializeField] float moveValue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveLeft()
    {
        transform.Translate(-1 * moveValue, 0, 0);
    }

    public void MoveRight()
    {
        transform.Translate(moveValue, 0, 0);
    }
}

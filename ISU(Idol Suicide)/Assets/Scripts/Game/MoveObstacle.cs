using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour {

    [SerializeField] Transform posReposition1, posReposition2, posReposition3, startPosition;
    [SerializeField] float speedObstacle, moveValue;
    int limitLeft, limitRight;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * speedObstacle;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Reposition()
    {
        speedObstacle += 0.05f;
        int position = Random.Range(0, 3);
        if(position == 1)
        {
            transform.position = posReposition1.position;
        }
        else if(position == 2)
        {
            transform.position = posReposition2.position;
        }
        else if (position == 3)
        {
            transform.position = posReposition3.position;
        }
        else
        {
            transform.position = posReposition1.position;
        }
        limitLeft =0; limitRight=0;
        GetComponent<Rigidbody>().velocity = transform.forward * speedObstacle;
    }

    public void RestartPosition()
    {
        transform.position = startPosition.position;
        limitLeft = 0; limitRight = 0;
        GetComponent<Rigidbody>().velocity = transform.forward * speedObstacle;
    }

    public void MoveLeft()
    {
        if(limitLeft < 6)
        {
            transform.Translate(-1 * moveValue, 0, 0);
            limitLeft++; limitRight--;
        }
    }

    public void MoveRight()
    {
        if(limitRight<6)
        {
            transform.Translate(moveValue, 0, 0);
            limitLeft--; limitRight++;
        }
    }
}

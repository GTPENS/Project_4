using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    public static int coin = 0;
    public static int MinusCoin = 0;

    [SerializeField] int thisCoin;
    [SerializeField] int thisMinus;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        thisCoin = coin;
        thisMinus = MinusCoin;
	}
}

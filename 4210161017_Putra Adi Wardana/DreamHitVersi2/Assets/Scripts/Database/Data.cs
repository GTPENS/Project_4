using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
    public static string _username, _email, _password;
    public static int _status;
    public static int _coin, _realMoney, _level;
    public static int _id;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

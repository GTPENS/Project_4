using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour {

    public static GlobalManager Instance;

    public AdsManager ADS;
    //public InAppManager IAP;
    public DataManager DATA;

    private void Awake()
    {
        SingletonChecker();
    }

    void SingletonChecker()
    {
        // pembuatan Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

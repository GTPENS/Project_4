using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

 
[System.Serializable]
public class SaveData
{
    //semua data yang mau di save, buat disini 
    public bool telahMembeliNoAds;


    public DateTime LastOpenTime;
}

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private SaveData m_SaveData;
       
    public SaveData SaveData
    {
        get
        {
            if(m_SaveData == null)
            {
                m_SaveData = LoadSaveData();
            }
            return m_SaveData;
        }
		set
		{
			m_SaveData = value;
		}
    }
    string SaveDataPath { get { return Application.persistentDataPath + "/GameData.dat"; } }
    public bool SaveDataCreated { get { return File.Exists(SaveDataPath); } }
        
    public bool SaveDataCreatedCompletely;
    public bool NewGame;

    void Awake()
    {
        NewGame = false;
        SaveDataCreatedCompletely = true;
        m_SaveData = LoadSaveData();
    }
        
    void OnApplicationQuit()
    {
        m_SaveData.LastOpenTime = DateTime.Now;
        SaveGameData();
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {

        }
        if (!focus)
        {
            m_SaveData.LastOpenTime = DateTime.Now;
            SaveGameData();
        }
    }

    SaveData LoadSaveData()
    {
        if (!SaveDataCreatedCompletely)
            return null;

        if (SaveDataCreated)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream data = File.Open(SaveDataPath, FileMode.Open);
            SaveData loadedData = (SaveData)bf.Deserialize(data);

            //do something to loaded data, if there any change here
			

            //end

            data.Close();
            return loadedData;
        }
        else
        {
            return NewSaveData();
        }
    }

    SaveData NewSaveData()
    {
        NewGame = true;
        SaveDataCreatedCompletely = false;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream data = File.Create(SaveDataPath);

        SaveData newData = new SaveData();

        //set starter value to saved data
        newData.telahMembeliNoAds = false;
        newData.LastOpenTime = DateTime.Now;

        //serialisasi data ke biner
        bf.Serialize(data, newData);
        data.Close();
        SaveDataCreatedCompletely = true;
        return newData;
    }

    public void SaveGameData(bool _checkTutorial = true)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream data = File.Create(SaveDataPath);
        bf.Serialize(data, m_SaveData);
        data.Close();
    }

}


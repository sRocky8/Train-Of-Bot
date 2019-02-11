using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataStorage : MonoBehaviour {

    public static DataStorage dataStorage;

    public float testFloat;

    private void Awake()
    {
        if (dataStorage == null)
        {
            DontDestroyOnLoad(gameObject);
            dataStorage = this;
        }
        else if(dataStorage != null)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream dataFile = File.Create(Application.persistentDataPath + "/TrainOfBotDataFile.dat");

        Data data = new Data();
        data.testFloat = 0.0f;

//        Data data = new Data
//        {
//            testFloat = 0.0f
//        };

        binaryFormatter.Serialize(dataFile, data);
        dataFile.Close();
    }

    public void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/TrainOfBotDataFile.dat")) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream dataFile = File.Open(Application.persistentDataPath + "/TrainOfBotDataFile.dat", FileMode.Open);
            Data data = (Data)binaryFormatter.Deserialize(dataFile);
            dataFile.Close();

            testFloat = data.testFloat;
        }
    }

    [Serializable]
    class Data
    {
        public float testFloat;
    }
}

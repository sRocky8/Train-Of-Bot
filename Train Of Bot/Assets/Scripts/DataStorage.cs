using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataStorage : MonoBehaviour {

    public static DataStorage dataStorage;

    public float testFloat;
    public Vector3 storageRoomBoxPos;
    public bool testCharacterCanRecieve;
    public bool testCharacterCanGive;

    //HAS STUFF BEEN TAKEN?
    public bool bottleOfBoltsTaken;
    public bool cabinetKeyTaken;
    public bool chefsSpoonTaken;
    public bool cookedMechanicalDinnerTaken;
    public bool earmuffsTaken;
    public bool frozenMechanicalDinnerTaken;
    public bool gasCanisterTaken;
    public bool passengersEyeTaken;
    public bool plungerTaken;
    public bool rattleTaken;
    public bool valveTaken;

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

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 80), "test float: " + testFloat);
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream dataFile = File.Create(Application.persistentDataPath + "/TrainOfBotDataFile.dat");

        Data data = new Data();
        data.testFloat = 0.0f;
        data.storageRoomBoxPos = StorageRoomBox.FindObjectOfType<Transform>().position;
        data.testCharacterCanRecieve = true;
        data.testCharacterCanGive = true;
        data.bottleOfBoltsTaken = BottleOfBolts.FindObjectOfType<BottleOfBolts>().taken;
        data.cabinetKeyTaken = CabinetKey.FindObjectOfType<CabinetKey>().taken;
        data.chefsSpoonTaken = ChefsSpoon.FindObjectOfType<ChefsSpoon>().taken;
        data.cookedMechanicalDinnerTaken = CookedMechanicalDinner.FindObjectOfType<CookedMechanicalDinner>().taken;
        data.earmuffsTaken = Earmuffs.FindObjectOfType<Earmuffs>().taken;
        data.frozenMechanicalDinnerTaken = FrozenMechanicalDinner.FindObjectOfType<FrozenMechanicalDinner>().taken;
        data.gasCanisterTaken = GasCanister.FindObjectOfType<GasCanister>().taken;
        data.passengersEyeTaken = PassengersEye.FindObjectOfType<PassengersEye>().taken;
        data.plungerTaken = Plunger.FindObjectOfType<Plunger>().taken;
        data.rattleTaken = Rattle.FindObjectOfType<Rattle>().taken;
        data.valveTaken = Valve.FindObjectOfType<Valve>().taken;

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
            storageRoomBoxPos = data.storageRoomBoxPos;
            testCharacterCanRecieve = data.testCharacterCanRecieve;
            testCharacterCanGive = data.testCharacterCanGive;

            //HAS STUFF BEEN TAKEN?
            bottleOfBoltsTaken = data.bottleOfBoltsTaken;
            cabinetKeyTaken = data.cabinetKeyTaken;
            chefsSpoonTaken = data.chefsSpoonTaken;
            cookedMechanicalDinnerTaken = data.cookedMechanicalDinnerTaken;
            earmuffsTaken = data.earmuffsTaken;
            frozenMechanicalDinnerTaken = data.frozenMechanicalDinnerTaken;
            gasCanisterTaken = data.gasCanisterTaken;
            passengersEyeTaken = data.passengersEyeTaken;
            plungerTaken = data.plungerTaken;
            rattleTaken = data.rattleTaken;
            valveTaken = data.valveTaken;
        }
    }

    [Serializable]
    class Data
    {
        //TESTING
        public float testFloat;
        public bool testCharacterCanRecieve;
        public bool testCharacterCanGive;

        //MISC OBJECTS
        public Vector3 storageRoomBoxPos;

        //HAS STUFF BEEN TAKEN?
        public bool bottleOfBoltsTaken;
        public bool cabinetKeyTaken;
        public bool chefsSpoonTaken;
        public bool cookedMechanicalDinnerTaken;
        public bool earmuffsTaken;
        public bool frozenMechanicalDinnerTaken;
        public bool gasCanisterTaken;
        public bool passengersEyeTaken;
        public bool plungerTaken;
        public bool rattleTaken;
        public bool valveTaken;

        //NPCS
    }
}

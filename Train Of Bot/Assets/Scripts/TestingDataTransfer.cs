using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDataTransfer : MonoBehaviour {

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 100, 100, 30), "float up"))
        {
            DataStorage.dataStorage.testFloat += 10;
        }
        if (GUI.Button(new Rect(10, 140, 100, 30), "float down"))
        {
            DataStorage.dataStorage.testFloat -= 10;
        }
    }
}

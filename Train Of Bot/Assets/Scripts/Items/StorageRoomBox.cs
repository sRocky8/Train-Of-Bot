using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomBox : MonoBehaviour {

    private void Awake()
    {
        try
        {
            transform.position = DataStorage.dataStorage.storageRoomBoxPos;
        }
        catch
        {
            return;
        }
    }

    private void OnDestroy()
    {
        DataStorage.dataStorage.storageRoomBoxPos = transform.position;
    }
}
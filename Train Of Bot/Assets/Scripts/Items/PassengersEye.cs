using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersEye : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.passengersEyeTaken;

            if (taken == true)
            {
                Destroy(gameObject);
            }
        }
        catch
        {
            return;
        }
    }

    private void OnDestroy()
    {
        DataStorage.dataStorage.passengersEyeTaken = taken;
    }
}

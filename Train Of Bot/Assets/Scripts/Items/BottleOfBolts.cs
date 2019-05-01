using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleOfBolts : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.bottleOfBoltsTaken;

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
        DataStorage.dataStorage.bottleOfBoltsTaken = taken;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMechanicalDinner : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.cookedMechanicalDinnerTaken;

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
        DataStorage.dataStorage.cookedMechanicalDinnerTaken = taken;
    }
}

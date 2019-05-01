using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.valveTaken;

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
        DataStorage.dataStorage.valveTaken = taken;
    }
}

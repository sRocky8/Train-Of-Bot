using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earmuffs : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.earmuffsTaken;

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
        DataStorage.dataStorage.earmuffsTaken = taken;
    }
}

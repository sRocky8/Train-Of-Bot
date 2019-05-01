using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetKey : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.cabinetKeyTaken;

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
        DataStorage.dataStorage.cabinetKeyTaken = taken;
    }
}

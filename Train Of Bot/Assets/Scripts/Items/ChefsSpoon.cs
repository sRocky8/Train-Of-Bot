using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefsSpoon : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.chefsSpoonTaken;

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
        DataStorage.dataStorage.chefsSpoonTaken = taken;
    }
}

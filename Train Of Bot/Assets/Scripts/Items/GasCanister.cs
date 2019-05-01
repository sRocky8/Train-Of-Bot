using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCanister : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.gasCanisterTaken;

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
        DataStorage.dataStorage.gasCanisterTaken = taken;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour {

    public bool taken = false;

    private void Awake()
    {
        try
        {
            taken = DataStorage.dataStorage.plungerTaken;

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
        DataStorage.dataStorage.plungerTaken = taken;
    }
}

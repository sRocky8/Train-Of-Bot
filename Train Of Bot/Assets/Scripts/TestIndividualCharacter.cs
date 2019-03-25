using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIndividualCharacter : CharacterDialogue {

    private void Awake()
    {
        try
        {
            canRecieveItem = DataStorage.dataStorage.testCharacterCanRecieve;
            canGiveItem = DataStorage.dataStorage.testCharacterCanGive;
        }
        catch
        {
            return;
        }
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (dialogueParameter == 0)
        {

        }
	}

    private void OnDestroy()
    {
        DataStorage.dataStorage.testCharacterCanRecieve = canRecieveItem;
        DataStorage.dataStorage.testCharacterCanGive = canGiveItem;
    }
}

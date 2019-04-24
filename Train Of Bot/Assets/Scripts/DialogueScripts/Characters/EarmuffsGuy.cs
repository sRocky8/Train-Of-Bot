using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarmuffsGuy : CharacterDialogue {

	void Start () {
        canRecieveItem = true;
        canGiveItem = true;
	}
	
	void Update () {
        CheckDialogueParam();
        TalkWithNPC();
	}

    void CheckDialogueParam()
    {
        if (playerMenuNum == 0 && canRecieveItem == true)
        {
            dialogueParameter = 0;
        }
        else if (playerMenuNum == 1 && canRecieveItem == true)
        {
            dialogueParameter = 1;
        }
        else if (playerMenuNum == 2 && playerInInventory == true)
        {
            for (i = 0; i < 8; i++)
            {
                if (playerInventoryNum == i)
                {
                    dialogueParameter = 2;
                }
            }
        }
        else if (playerMenuNum == 0 && canRecieveItem == true)
        {
            dialogueParameter = 3;
        }
        else if (playerMenuNum == 0 && canRecieveItem == true)
        {
            dialogueParameter = 4;
        }
        else if (playerMenuNum == 0 && canRecieveItem == true)
        {
            dialogueParameter = 5;
        }
    }
}

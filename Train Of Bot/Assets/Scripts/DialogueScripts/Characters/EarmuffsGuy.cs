﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarmuffsGuy : CharacterDialogue {

	void Start () {
        canRecieveItem = true;
        canGiveItem = true;
        playerInventorySlot = FindObjectOfType<PlayerController>().inventorySlot;
    }
	
	void Update () {
        CheckDialogueParam();
        TalkWithNPC();
	}

    void CheckDialogueParam()
    {
        playerInventorySlot = FindObjectOfType<PlayerController>().inventorySlot;
        if (playerMenuNum == 0 && canRecieveItem == true)
        {
            dialogueParameter = 0;
        }
        else if (playerMenuNum == 1 && canRecieveItem == true)
        {
            dialogueParameter = 1;
        }
        else if (playerMenuNum == 2)
        {
            for (int i = 0; i < playerInventorySlot.Length; i++)
            {
                if (playerInventorySlot[i] == (int)Items.Earmuffs)
                {
                    if (playerInInventory == true)
                    {
                        dialogueParameter = 2;
                        FindObjectOfType<PlayerController>().inventorySlot[i] = 0;
                        FindObjectOfType<PlayerController>().inventory[i].sprite = FindObjectOfType<PlayerController>().inventoryImage[0];
                        canRecieveItem = false;
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        else if (playerMenuNum == 0 && canRecieveItem == false)
        {
            dialogueParameter = 3;
        }
        else if (playerMenuNum == 1 && canRecieveItem == false)
        {
            dialogueParameter = 4;
        }
        else if (playerMenuNum == 2 && playerInInventory == true)
        {
            for (int i = 0; i < playerInventorySlot.Length; i++)
            {
                if (playerInventorySlot[i] != (int)Items.Earmuffs)
                {
                    dialogueParameter = 5;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingToInteractWith : CharacterDialogue {

	// Use this for initialization
	void Start () {
        canGiveItem = false;
        canRecieveItem = false;
	}

    // Update is called once per frame
    //void Update () {
    //	if(playerInMenu == true && playerMenuNum == 0)
    //       {
    //           dialogueParameter = 0;
    //       }
    //       else if (playerInMenu == true && playerMenuNum == 1)
    //       {
    //           dialogueParameter = 1;
    //       }
    //       else if (playerInInventory == true && playerInventoryNum == 1)
    //       {
    //           dialogueParameter = 2;
    //       }
    //}

    void Update()
    {
        //RaycastForPlayer();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance, layerMask1))
        {
            Debug.Log("hit Player");
            lookingAtPlayer = true;
        }
        else
        {
            lookingAtPlayer = false;
        }

        TalkWithNPC();
        if (playerInMenu == true && playerMenuNum == 0)
        {
            dialogueParameter = 0;
        }
        else if (playerInMenu == true && playerMenuNum == 1)
        {
            dialogueParameter = 1;
        }
        else if (playerInInventory == true && playerInventoryNum == 1)
        {
            dialogueParameter = 2;
        }
    }
}

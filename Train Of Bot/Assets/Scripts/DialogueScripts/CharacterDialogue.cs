using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour {

    //Public Variables
    [HideInInspector] public bool endedDialogue;
    [HideInInspector] public bool inConversation;
    [HideInInspector] public bool canGiveItem;
    [HideInInspector] public bool canRecieveItem;
    public float rayMaxDistance;
    public int dialogueParameter;
    public DialogueClass npcDialogue;

    //Protected Variables
    protected RaycastHit hit;

    protected int layerMask1;
    protected bool lookingAtPlayer;
    protected bool playerLooking;

    protected bool playerInMenu;
    protected int playerMenuNum;

    protected bool playerInInventory;
    protected int playerInventoryNum;


	void Start () {
        layerMask1 = 1 << 10;
        inConversation = false;
        endedDialogue = FindObjectOfType<DialogueController>().endedDialogue;
        playerInMenu = FindObjectOfType<PlayerController>().inMenu;
        playerMenuNum = FindObjectOfType<PlayerController>().highlightedPos;
        playerInInventory = FindObjectOfType<PlayerController>().inInventory;
        playerInventoryNum = FindObjectOfType<PlayerController>().inventoryCursorPos;
        //TESTING DIALOGUE PARAMETER VARIABLE
        //dialogueParameter = 1;

    }
	
	void Update () {
        TalkWithNPC();
	}

    protected void TalkWithNPC()
    {
        playerInMenu = FindObjectOfType<PlayerController>().inMenu;
        playerMenuNum = FindObjectOfType<PlayerController>().highlightedPos;
        playerLooking = FindObjectOfType<PlayerController>().lookingAtSpeaker;
        playerInInventory = FindObjectOfType<PlayerController>().inInventory;
        playerInventoryNum = FindObjectOfType<PlayerController>().inventoryCursorPos;

        RaycastForPlayer();

        if (inConversation == true && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialogue();
        }

        if (lookingAtPlayer == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (inConversation == false && playerLooking == true)
            {
                if (playerInMenu == true && playerMenuNum == 0)
                {
                    ActivateDialogue();
                }
            }
        }

        endedDialogue = FindObjectOfType<DialogueController>().endedDialogue;

        if (endedDialogue == true)
        {
            inConversation = false;
        }

        //TEST
        if (gameObject.name == "Main_Char_Model")
        {
            dialogueParameter = 1;
        }
    }

    protected void RaycastForPlayer()
    {
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance, layerMask1))
        {
            Debug.Log("hit Player");
            lookingAtPlayer = true;
        }
        else
        {
            lookingAtPlayer = false;
        }
    }

    public void ActivateDialogue()
    {
        FindObjectOfType<DialogueController>().StartDialogue(npcDialogue, dialogueParameter);
        inConversation = true;
    }

    public void ContinueDialogue()
    {
        FindObjectOfType<DialogueController>().ShowNextLine();
        Debug.Log("Continued Dialogue");
    }
}
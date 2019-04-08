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

    //Private Variables
    private int layerMask1;
//    private int layerMask2;
    private bool lookingAtPlayer;
    private bool playerLooking;
    private bool playerInMenu;
    private int playerMenuNum;

	void Start () {
        layerMask1 = 1 << 10;
//        layerMask2 = 1 << 11;
        inConversation = false;
        endedDialogue = FindObjectOfType<DialogueController>().endedDialogue;
        playerInMenu = FindObjectOfType<PlayerController>().inMenu;
        playerMenuNum = FindObjectOfType<PlayerController>().highlightedPos;

        //TESTING DIALOGUE PARAMETER VARIABLE
        //dialogueParameter = 1;

    }
	
	void Update () {
        playerInMenu = FindObjectOfType<PlayerController>().inMenu;
        playerMenuNum = FindObjectOfType<PlayerController>().highlightedPos;
        playerLooking = FindObjectOfType<PlayerController>().lookingAtSpeaker;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance, layerMask1))
        {
            Debug.Log("hit Player");
            lookingAtPlayer = true;
        }
        else
        {
            lookingAtPlayer = false;
        }

        if (inConversation == true && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialogue();
        }

        if (lookingAtPlayer == true && Input.GetKeyDown(KeyCode.Space))
        {
            if(inConversation == false && playerLooking == true)
            {
                if (playerInMenu == true && playerMenuNum == 0) {
                    ActivateDialogue();
                }
            }
        }

        endedDialogue = FindObjectOfType<DialogueController>().endedDialogue;
        
        if(endedDialogue == true)
        {
            inConversation = false;
        }

        

        //TEST
        if(gameObject.tag == "Main_Char_Model")
        {


            dialogueParameter = 0;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour {

    //Public Variables
    [HideInInspector] public bool endedDialogue;
    [HideInInspector] public bool inConversation;
    public float rayMaxDistance;
    public int dialogueParameter;
    public DialogueClass npcDialogue;

    //Private Variables
    private int layerMask1;
    private int layerMask2;
    private bool lookingAtPlayer;
    private bool playerLooking;

	void Start () {
        layerMask1 = 1 << 10;
        layerMask2 = 1 << 11;
        inConversation = false;
        endedDialogue = FindObjectOfType<DialogueController>().endedDialogue;

        //TESTING DIALOGUE PARAMETER VARIABLE
        dialogueParameter = 1;

	}
	
	void Update () {
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

        if (lookingAtPlayer == true && Input.GetKeyDown(KeyCode.E))
        {
            if(inConversation == false && playerLooking == true)
            {
                ActivateDialogue();
            }
        }

        endedDialogue = FindObjectOfType<DialogueController>().endedDialogue;
        
        if(endedDialogue == true)
        {
            inConversation = false;
        }

        if(inConversation == true && Input.GetKeyDown(KeyCode.E))
        {
            ContinueDialogue();
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

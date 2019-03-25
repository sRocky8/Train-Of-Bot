using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Public Variables
    public static PlayerController player;

    [HideInInspector] public bool inConversation;
    [HideInInspector] public bool lookingAtSpeaker;
    public float speed;
    public float rayMaxDistance;
    public CharacterDialogue characterDialogueScript;

    //Private Variables
//    private bool canMoveRight;
//    private bool canMoveForward;
//    private Rigidbody rb;
    private int layerMask1;
    private int layerMask2;
    private int currentScene;
    private GameObject choiceUI;
    private GameObject highlightChoice;
    private bool inMenu;

    //0 is y = 25, 1 is y = 0, 2 is y = -25
    private int highlightedPos;

    private void Awake()
    {
        if (player == null)
        {
            DontDestroyOnLoad(gameObject);
            player = this;
        }
        else if (player != null)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
//        rb = GetComponent<Rigidbody>();
//        canMoveRight = true;
//        canMoveForward = true;

        choiceUI = gameObject.transform.Find("ChoiceImage").gameObject;
        highlightChoice = gameObject.transform.Find("Highlight").gameObject;

        layerMask1 = 1 << 9;
        lookingAtSpeaker = false;
        inConversation = false;
        inMenu = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (inMenu == false)
        {
            if(Input.GetKeyDown(KeyCode.E) == true)
            {

                inMenu = true;
                choiceUI.SetActive(true);

            }
            if (inConversation == false)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                if (moveHorizontal > 0.0f)
                {
                    transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f), Space.World);
                    transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                }
                else if (moveHorizontal < 0.0f)
                {

                    transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f), Space.World);
                    transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                }
                else if (moveVertical > 0.0f)
                {
                    transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f), Space.World);
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else if (moveVertical < 0.0f)
                {

                    transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f), Space.World);
                    transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                }
            }
        }



        if (inMenu == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inMenu = false;
                choiceUI.SetActive(false);
            }


        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance, layerMask1))
        {
            inConversation = hit.collider.gameObject.GetComponent<CharacterDialogue>().inConversation;
            lookingAtSpeaker = true;
        }
        else
        {
            lookingAtSpeaker = false;
        }
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "NextScene")
        {
            SceneManager.LoadScene(1);
        }
        if(other.tag == "PreviousScene")
        {
            SceneManager.LoadScene(0);
        }
    }
}

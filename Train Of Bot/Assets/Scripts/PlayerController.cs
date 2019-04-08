using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Public Variables
    public static PlayerController player;

    [HideInInspector] public bool inConversation;
    [HideInInspector] public bool lookingAtSpeaker;

    //0 is y = 25, 1 is y = 0, 2 is y = -25
    [HideInInspector] public int highlightedPos;
    [HideInInspector] public bool inMenu;
    [HideInInspector] public bool inInventory;
    public float speed;
    public float rayMaxDistance;
    public CharacterDialogue characterDialogueScript;

    public GameObject choiceUI;
    public GameObject highlightChoice;


    //Private Variables
    //    private bool canMoveRight;
    //    private bool canMoveForward;
    //    private Rigidbody rb;
    private int layerMask1;
    private int layerMask2;
    private int currentScene;
    
    

    private RectTransform highlightChoiceV3;



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

        //        choiceUI = gameObject.transform.Find("ChoiceImage").gameObject;
        //        highlightChoice = gameObject.transform.Find("Highlight").gameObject;

        highlightChoiceV3 = highlightChoice.GetComponent<RectTransform>();

        layerMask1 = 1 << 9;
        lookingAtSpeaker = false;
        inConversation = false;
        inMenu = false;
        inInventory = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (inInventory == false)
        {
            if (inMenu == false)
            {
                if (Input.GetKeyDown(KeyCode.E) == true)
                {

                    inMenu = true;
                    choiceUI.SetActive(true);
                    highlightedPos = 0;

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
        }


        if (inMenu == true && inInventory == false)
        {
            //EXIT
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inMenu = false;
                choiceUI.SetActive(false);
            }
            //BEEP
            if(highlightedPos == 0)
            {
                if (Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
                {
                    highlightedPos = 2;
                }
                else if (Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    highlightedPos = 1;
                }
                else if (Input.GetKeyDown(KeyCode.E) == true)
                {
//                    inMenu = false;
//                    choiceUI.SetActive(false);
                }
            }
            //TAKE
            else if (highlightedPos == 1)
            {
                if (Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
                {
                    highlightedPos = 0;
                }
                else if (Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    highlightedPos = 2;
                }
                else if (Input.GetKeyDown(KeyCode.E) == true)
                {

                }
            }
            //GIVE
            else if (highlightedPos == 2)
            {
                if (Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
                {
                    highlightedPos = 1;
                }
                else if (Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    highlightedPos = 0;
                }
                else if (Input.GetKeyDown(KeyCode.E) == true)
                {

                }
            }

            switch (highlightedPos)
            {
                case 0:
                    highlightChoiceV3.anchoredPosition = new Vector3(0.0f, 25.0f, 0.0f);
                    break;
                case 1:
                    highlightChoiceV3.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                case 2:
                    highlightChoiceV3.anchoredPosition = new Vector3(0.0f, -25.0f, 0.0f);
                    break;
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

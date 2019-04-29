using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Items
{
    Nothing = 0,
    Earmuffs,
    TP,
    CabinetKey,
    BottleOfBolts,
    GasCanister,
    FrozenMechanicalDinner,
    CookedMechanicalDinner,
    Plunger,
    PassengersEye,
    Valve,
    ChefsSpoon,
    Rattle
};

public class PlayerController : MonoBehaviour {

    //Public Variables
    public static PlayerController player;
    //public Items item;
    public int[] inventorySlot;
    public Image[] inventory;
    public Sprite[] inventoryImage;
    public Items[] items;
    [HideInInspector] public bool inConversation;
    [HideInInspector] public bool lookingAtSpeaker;

    //0 is y = 25, 1 is y = 0, 2 is y = -25
    [HideInInspector] public int highlightedPos;

    /*
     * 0 is x = -150, y = 60
     * 1 is x = -50, y = 60
     * 2 is x = 50, y = 60
     * 3 is x = 150, y = 60
     * 4 is x = -150, y = -60
     * 5 is x = -50, y = -60
     * 6 is x = 50, y = -60
     * 7 is x = 150, y = -60
     */
    [HideInInspector] public int inventoryCursorPos;
    [HideInInspector] public bool inMenu;
    [HideInInspector] public bool inInventory;
    [HideInInspector] public bool givingItem;
    public float speed;
    public float rayMaxDistance;
    public CharacterDialogue characterDialogueScript;

    public GameObject choiceUI;
    public GameObject highlightChoice;
    public GameObject inventoryGameObject;
    public GameObject inventoryCursor;

    public Animator playerAnimator;

    //Private Variables
    //    private bool canMoveRight;
    //    private bool canMoveForward;
    //    private Rigidbody rb;
    private bool fullInventory;
    private bool lookingAtInventory;
    private int layerMask1;
    private int layerMask2;
    private int currentScene;
    private RectTransform highlightChoiceV3;
    private RectTransform inventoryChoice;
    private bool moving;

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

    void Start () {
        //        rb = GetComponent<Rigidbody>();
        //        canMoveRight = true;
        //        canMoveForward = true;

        currentScene = SceneManager.GetActiveScene().buildIndex;
        playerAnimator = gameObject.GetComponent<Animator>();

        highlightChoiceV3 = highlightChoice.GetComponent<RectTransform>();
        inventoryChoice = inventoryCursor.GetComponent<RectTransform>();

        layerMask1 = 1 << 9;
        layerMask2 = 1 << 11;
        lookingAtSpeaker = false;
        inConversation = false;
        inMenu = false;
        inInventory = false;
        givingItem = false;

        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i].sprite = inventoryImage[0];
        }    
	}
	
	void Update () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (givingItem == true)
        {
            givingItem = false;
        }
        if (inInventory == false)
        {
            if (inMenu == false)
            {
                if (Input.GetKeyDown(KeyCode.E) == true && inConversation == false)
                {
                    if (lookingAtInventory == false)
                    {

                        inMenu = true;
                        choiceUI.SetActive(true);
                        highlightedPos = 0;


                    }
                }
                if (inConversation == false)
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");

                    if (moveHorizontal > 0.0f)
                    {
                        transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f), Space.World);
                        transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                        playerAnimator.Play("Walk");
                        playerAnimator.SetBool("moving", true);
                    }
                    else if (moveHorizontal < 0.0f)
                    {
                        transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f), Space.World);
                        transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                        playerAnimator.Play("Walk");
                        playerAnimator.SetBool("moving", true);
                    }
                    else if (moveVertical > 0.0f)
                    {
                        transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f), Space.World);
                        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                        playerAnimator.Play("Walk");
                        playerAnimator.SetBool("moving", true);
                    }
                    else if (moveVertical < 0.0f)
                    {
                        transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f), Space.World);
                        transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                        playerAnimator.Play("Walk");
                        playerAnimator.SetBool("moving", true);
                    }
                    else
                    {
                        playerAnimator.SetBool("moving", false);
                    }

                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        lookingAtInventory = true;
                        inventoryGameObject.SetActive(true);
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        lookingAtInventory = false;
                        inventoryGameObject.SetActive(false);
                    }
                }
            }
        }
        //INVENTORY MENU
        if (inMenu == true && inInventory == true)
        {
            //EXIT
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inventoryCursor.SetActive(false);
                inventoryGameObject.SetActive(false);
                inInventory = false;
            }

            //ITEM 1
            if (inventoryCursorPos == 0)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 4;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 1;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 3;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }
            //ITEM 2
            else if (inventoryCursorPos == 1)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 5;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 2;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }
            //ITEM 3
            else if (inventoryCursorPos == 2)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 6;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 3;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }
            //ITEM 4
            else if (inventoryCursorPos == 3)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 7;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 0;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }
            //ITEM 5
            else if (inventoryCursorPos == 4)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 0;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 5;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 7;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }
            //ITEM 6
            else if (inventoryCursorPos == 5)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 1;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 6;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 4;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }
            //ITEM 7
            else if (inventoryCursorPos == 6)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 2;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 7;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 5;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }
            //ITEM 8
            else if (inventoryCursorPos == 7)
            {
                if ((Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true) ||
                    ((Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)))
                {
                    inventoryCursorPos = 3;
                }
                else if (Input.GetKeyDown(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inventoryCursorPos = 4;
                }
                else if (Input.GetKeyDown(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inventoryCursorPos = 6;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    CloseInventoryMenu();
                }
            }

            switch (inventoryCursorPos)
            {
                case 0:
                    inventoryChoice.anchoredPosition = new Vector3(-150.0f, 55.0f, 0.0f);
                    break;
                case 1:
                    inventoryChoice.anchoredPosition = new Vector3(-50.0f, 55.0f, 0.0f);
                    break;
                case 2:
                    inventoryChoice.anchoredPosition = new Vector3(50.0f, 55.0f, 0.0f);
                    break;
                case 3:
                    inventoryChoice.anchoredPosition = new Vector3(150.0f, 55.0f, 0.0f);
                    break;
                case 4:
                    inventoryChoice.anchoredPosition = new Vector3(-150.0f, -55.0f, 0.0f);
                    break;
                case 5:
                    inventoryChoice.anchoredPosition = new Vector3(-50.0f, -55.0f, 0.0f);
                    break;
                case 6:
                    inventoryChoice.anchoredPosition = new Vector3(50.0f, -55.0f, 0.0f);
                    break;
                case 7:
                    inventoryChoice.anchoredPosition = new Vector3(150.0f, -55.0f, 0.0f);
                    break;
            }
        }

        //MENU
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
                else if (Input.GetKeyDown(KeyCode.Space) == true && lookingAtSpeaker == true)
                {
                    inMenu = false;
                    choiceUI.SetActive(false);
                }
            }
            //TAKE
            else if (highlightedPos == 1)
            {
                CheckForZeros();
                if (Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
                {
                    highlightedPos = 0;
                }
                else if (Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    highlightedPos = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Space) == true && fullInventory == false)
                {
                    AddItem();
                    inMenu = false;
                    choiceUI.SetActive(false);
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
                else if (Input.GetKeyDown(KeyCode.Space) == true)
                {
                    inInventory = true;
                    inventoryGameObject.SetActive(true);
                    inventoryCursor.SetActive(true);
                    inventoryCursorPos = 0;
                }
            }

            switch (highlightedPos)
            {
                case 0:
                    highlightChoiceV3.anchoredPosition = new Vector3(0.0f, 41.39f, 0.0f);
                    break;
                case 1:
                    highlightChoiceV3.anchoredPosition = new Vector3(0.0f, 1.1f, 0.0f);
                    break;
                case 2:
                    highlightChoiceV3.anchoredPosition = new Vector3(0.0f, -37.2f, 0.0f);
                    break;
            }

        }



        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance, layerMask1))
        {
            Debug.Log("Player Looking at NPC");
            WhatDialogueClass(hit);
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
        if (other.tag == "NextScene" && inMenu == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentScene + 1);
            }
        }
        if(other.tag == "PreviousScene" && inMenu == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentScene - 1);
            }
        }
    }

    private void CheckForZeros()
    {
        //int itemToNumber = (int)item;
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            if(inventorySlot[i] == 0)
            {
                fullInventory = false;
                break;
            }
            else
            {
                fullInventory = true;
            }
        }
    }

    private void AddItem()
    {
        //int itemToNumber = (int)item;
        RaycastHit itemHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out itemHit, rayMaxDistance, layerMask2))
        {
            Debug.Log("HitObject");
            if (itemHit.transform.tag.Equals("Earmuffs"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Earmuffs;
                        inventory[i].sprite = inventoryImage[(int)Items.Earmuffs];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("CabinetKey"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.CabinetKey;
                        inventory[i].sprite = inventoryImage[(int)Items.CabinetKey];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("BottleOfBolts"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.BottleOfBolts;
                        inventory[i].sprite = inventoryImage[(int)Items.BottleOfBolts];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("GasCanister"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.GasCanister;
                        inventory[i].sprite = inventoryImage[(int)Items.GasCanister];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("FrozenMechanicalDinner"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.FrozenMechanicalDinner;
                        inventory[i].sprite = inventoryImage[(int)Items.FrozenMechanicalDinner];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("CookedMechanicalDinner"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.CookedMechanicalDinner;
                        inventory[i].sprite = inventoryImage[(int)Items.CookedMechanicalDinner];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("Plunger"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Plunger;
                        inventory[i].sprite = inventoryImage[(int)Items.Plunger];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("PassengersEye"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.PassengersEye;
                        inventory[i].sprite = inventoryImage[(int)Items.PassengersEye];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("Valve"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Valve;
                        inventory[i].sprite = inventoryImage[(int)Items.Valve];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("ChefsSpoon"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.ChefsSpoon;
                        inventory[i].sprite = inventoryImage[(int)Items.ChefsSpoon];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
            if (itemHit.transform.tag.Equals("Rattle"))
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Rattle;
                        inventory[i].sprite = inventoryImage[(int)Items.Rattle];
                        Destroy(itemHit.transform.gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("ObjectNotHit");
        }
    }

    private void WhatDialogueClass(RaycastHit hit)
    {
        if (hit.transform.name == "EarmuffsGuy")
        {
            inConversation = hit.collider.gameObject.GetComponent<EarmuffsGuy>().inConversation;
        }
        else if (hit.transform.name == "Dog")
        {
            inConversation = hit.collider.gameObject.GetComponent<EarmuffsGuy>().inConversation;
        }
        else if (hit.transform.name == "Mom")
        {
            inConversation = hit.collider.gameObject.GetComponent<EarmuffsGuy>().inConversation;
        }
        else if (hit.transform.name == "Boy")
        {
            inConversation = hit.collider.gameObject.GetComponent<EarmuffsGuy>().inConversation;
        }
        else if (hit.transform.name == "Chef")
        {
            inConversation = hit.collider.gameObject.GetComponent<EarmuffsGuy>().inConversation;
        }
        else if (hit.transform.name == "ToiletMan")
        {
            inConversation = hit.collider.gameObject.GetComponent<EarmuffsGuy>().inConversation;
        }
        else if (hit.transform.name == "Main_Char_Model")
        {
            inConversation = hit.collider.gameObject.GetComponent<EarmuffsGuy>().inConversation;
        }
    }

    private void CloseInventoryMenu()
    {
        inMenu = false;
        choiceUI.SetActive(false);
        inInventory = false;
        inventoryCursor.SetActive(false);
        inventoryGameObject.SetActive(false);
        inventoryCursorPos = 0;
    }
}
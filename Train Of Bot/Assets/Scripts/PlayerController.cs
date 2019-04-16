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
    public Items item;
    public int[] inventorySlot;
    public Image[] inventory;
    public Sprite[] inventoryImage;
    public Items[] items;
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
    private bool fullInventory;
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

    void Start () {
        //        rb = GetComponent<Rigidbody>();
        //        canMoveRight = true;
        //        canMoveForward = true;

        highlightChoiceV3 = highlightChoice.GetComponent<RectTransform>();

        layerMask1 = 1 << 9;
        layerMask2 = 1 << 11;
        lookingAtSpeaker = false;
        inConversation = false;
        inMenu = false;
        inInventory = false;

        int itemToNumber = (int)item;

        switch (itemToNumber) {
            case 0:
                break;
        }

        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i].sprite = inventoryImage[0];
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (inInventory == false)
        {
            if (inMenu == false)
            {
                if (Input.GetKeyDown(KeyCode.E) == true && inConversation == false)
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
        int itemToNumber = (int)item;
        RaycastHit itemHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out itemHit, rayMaxDistance, layerMask2))
        {
            if (itemHit.collider.tag == "Earmuffs")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Earmuffs;
                        inventory[i].sprite = inventoryImage[(int)Items.Earmuffs];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "CabinetKey")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.CabinetKey;
                        inventory[i].sprite = inventoryImage[(int)Items.CabinetKey];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "BottleOfBolts")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.BottleOfBolts;
                        inventory[i].sprite = inventoryImage[(int)Items.BottleOfBolts];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "GasCanister")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.GasCanister;
                        inventory[i].sprite = inventoryImage[(int)Items.GasCanister];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "FrozenMechanicalDinner")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.FrozenMechanicalDinner;
                        inventory[i].sprite = inventoryImage[(int)Items.FrozenMechanicalDinner];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "CookedMechanicalDinner")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.CookedMechanicalDinner;
                        inventory[i].sprite = inventoryImage[(int)Items.CookedMechanicalDinner];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "Plunger")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Plunger;
                        inventory[i].sprite = inventoryImage[(int)Items.Plunger];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "PassengersEye")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.PassengersEye;
                        inventory[i].sprite = inventoryImage[(int)Items.PassengersEye];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "Valve")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Valve;
                        inventory[i].sprite = inventoryImage[(int)Items.Valve];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "ChefsSpoon")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.ChefsSpoon;
                        inventory[i].sprite = inventoryImage[(int)Items.ChefsSpoon];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
            if (itemHit.collider.tag == "Rattle")
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i] == 0)
                    {
                        inventorySlot[i] = (int)Items.Rattle;
                        inventory[i].sprite = inventoryImage[(int)Items.Rattle];
                        Destroy(itemHit.collider);
                        break;
                    }
                }
            }
        }
    }
}

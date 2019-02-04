using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Public Variables
    public float speed;

    //Private Variables
    private bool canMoveRight;
    private bool canMoveForward;
    private Rigidbody rb;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        canMoveRight = true;
        canMoveForward = true;
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0.0f)
        {
            if (canMoveRight)
            {
                canMoveForward = false;
                transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f));
            }
        }
        else
        {
            rb.velocity.x = 0.0f;
            canMoveForward = true;
        }

        if (moveVertical != 0.0f)
        {
            if (canMoveForward == true)
            {
                canMoveRight = false;
                transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f));
            }
        }
        else
        {
            canMoveRight = true;
        }
    }

    private void FixedUpdate()
    {

    }
}

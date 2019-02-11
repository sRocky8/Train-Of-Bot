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
    private CharacterController characterController;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        canMoveRight = true;
        canMoveForward = true;
	}
	
	// Update is called once per frame
	void Update () {
//        Vector3 direction = Vector3.zero; 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal > 0.0f)
        {
//            direction.x = 1.0f;
            transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f));
        }
        else if (moveHorizontal < 0.0f)
        {
//            direction.x = -1.0f;
            transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f));
        }
        else if (moveVertical > 0.0f)
        {
//            direction.z = 1.0f;
            transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f));
        }
        else if (moveVertical < 0.0f)
        {
//            direction.z = -1.0f;
            transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f));
        }
//        transform.Translate(direction * (speed / 100.0f) * Time.deltaTime, Space.World);









        //if (moveHorizontal != 0.0f)
        //{
        //    if (canMoveRight)
        //    {
        //        canMoveForward = false;
        //        transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f));
        //    }
        //}
        //else
        //{
        //    characterController.velocity.x = 0.0f;
        //    canMoveForward = true;
        //}

        //if (moveVertical != 0.0f)
        //{
        //    if (canMoveForward == true)
        //    {
        //        canMoveRight = false;
        //        transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f));
        //    }
        //}
        //else
        //{
        //    canMoveRight = true;
        //}
    }

    private void FixedUpdate()
    {

    }
}

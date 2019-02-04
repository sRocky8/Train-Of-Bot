using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Public Variables
    public float speed;

    //Private Variables
    private bool canTurnRight;
    private bool canTurnLeft;
    
	// Use this for initialization
	void Start () {
        canTurnRight = true;
        canTurnLeft = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(moveVertical == 0.0f)
        {
            transform.Translate(Vector3.right * moveHorizontal * (speed / 100.0f));
        }
        if(moveHorizontal == 0.0f)
        {
            transform.Translate(Vector3.forward * moveVertical * (speed / 100.0f));
        }
    }
}

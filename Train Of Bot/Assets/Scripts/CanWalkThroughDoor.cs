using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanWalkThroughDoor : MonoBehaviour {

    public GameObject doorTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Box")
        {
            doorTrigger.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

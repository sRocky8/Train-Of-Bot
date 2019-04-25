﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //Public Variables
    public Vector3 offset;

    //Private Variables
    private Transform playerTransform;

    void Start () {
        playerTransform = FindObjectOfType<PlayerController>().transform;
	}
	
	void Update () {
        transform.position = playerTransform.position - offset;
	}
}
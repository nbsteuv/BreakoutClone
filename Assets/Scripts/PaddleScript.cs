using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("Left");
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("Right");
        }

        //GetButtonDown fires once per press, GetButton fires over and over like GetAxis

        //if (Input.GetButtonDown("Jump"))
        //{
        //    Debug.Log("Jump");
        //}
        if (Input.GetButton("Jump"))
        {
            Debug.Log("Jump");
        }
    }
}

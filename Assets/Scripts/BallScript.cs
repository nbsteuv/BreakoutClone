using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //Fire the ball
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(100f, 300f, 0);
        }
    }
}

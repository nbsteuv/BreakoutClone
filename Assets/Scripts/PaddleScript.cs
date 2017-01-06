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
            transform.Translate(-10f * Time.deltaTime, 0, 0);
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("Right");
            transform.Translate(10f * Time.deltaTime, 0, 0);
        }
    }
}

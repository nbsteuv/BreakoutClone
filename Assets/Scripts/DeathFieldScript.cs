using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        BallScript ballScript = other.GetComponent<BallScript>();
        if (ballScript)
        {
            ballScript.Die();
        }
    }
}

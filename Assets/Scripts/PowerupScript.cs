using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody>().AddTorque( Vector3.forward * 10f );	
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

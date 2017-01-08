using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

    public int pointValue = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        GameObject.Find("Paddle").GetComponent<PaddleScript>().AddPoint(pointValue) ;
    } 
}

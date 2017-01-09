﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickScript : MonoBehaviour {

    static int numBricks = 0;
    public int pointValue = 1;
    public int hitPoints = 1;

	// Use this for initialization
	void Start () {
        numBricks++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        hitPoints--;
        if(hitPoints <= 0)
        {
            Die();
        }
        
    } 

    void Die()
    {
        Destroy(gameObject);
        PaddleScript paddleScript = GameObject.Find("Paddle").GetComponent<PaddleScript>();
        paddleScript.AddPoint(pointValue);
        numBricks--;
        if (numBricks <= 0)
        {
            SceneManager.LoadScene("Level2");
            paddleScript.SpawnBall();
        }
    }
}

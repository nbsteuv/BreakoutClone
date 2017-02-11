using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

    public Material extraBallPowerupMaterial;
    public Material extraLifePowerupMaterial;

    System.Random random = new System.Random();
    PaddleScript paddleScript;
    delegate void PowerupAction();
    struct Powerup
    {
        public Material powerupMaterial;
        public PowerupAction powerupAction;
        public Powerup(Material material, PowerupAction action)
        {
            powerupMaterial = material;
            powerupAction = action;
        }
    }

    List<Powerup> powerups = new List<Powerup>();
    Powerup powerup;

    // Use this for initialization
    void Start () {
        GameObject paddleObject = GameObject.Find("Paddle");
        paddleScript = paddleObject.GetComponent<PaddleScript>();
        powerups.Add(new Powerup(extraBallPowerupMaterial, SpawnExtraBall));
        powerups.Add(new Powerup(extraLifePowerupMaterial, AddLife));
        SetPowerup();
        ApplyMaterial(powerup.powerupMaterial);
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody>().AddTorque( Vector3.forward * 10f );	
	}

    void SetPowerup()
    {
        int index = random.Next(powerups.Count);
        powerup = powerups[index];
    }

    private void OnCollisionEnter(Collision collision)
    {
        powerup.powerupAction();
        Destroy(gameObject);
    }

    void ApplyMaterial(Material material)
    {
        Renderer powerupRenderer = gameObject.GetComponent<Renderer>();
        powerupRenderer.material = material;
    }

    void SpawnExtraBall()
    {
        paddleScript.SpawnBall();
    }

    void AddLife()
    {
        paddleScript.AddLife();
    }
}

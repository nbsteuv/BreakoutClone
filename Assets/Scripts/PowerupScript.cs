using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

    public Material extraBallPowerupMaterial;
    public Material extraLifePowerupMaterial;

    System.Random random = new System.Random();

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
        powerups.Add(new Powerup(extraBallPowerupMaterial, OnSpawnExtraBall));
        powerups.Add(new Powerup(extraLifePowerupMaterial, OnAddLife));
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

    public delegate void PowerupBroadcastAction();
    public event PowerupBroadcastAction SpawnExtraBall;

    void OnSpawnExtraBall()
    {
        if(SpawnExtraBall != null)
        {
            SpawnExtraBall();
        }
    }

    public event PowerupBroadcastAction AddLife;

    void OnAddLife()
    {
        if(AddLife != null)
        {
            AddLife();
        }
    }
}

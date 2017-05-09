﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickScript : MonoBehaviour {

    public static int numBricks = 0;

    public int pointValue = 1;
    public int hitPoints = 1;
    public bool topAttackOnly = false;
    public GameObject powerupPrefab;
    public int powerupPercentChance;

    public delegate void BrickDeathAction(object source, BrickEventArgs args);
    public event BrickDeathAction BrickDeath;

	void Start () {
        numBricks++;
	}
	
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (!topAttackOnly)
        {
            TakeHit();
        }
    } 

    public void TakeHit()
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        numBricks--;
        OnBrickDeath();
        Destroy(gameObject);
        if (numBricks > 0 && powerupWillSpawn())
        {
            SpawnPowerup();
        }
    }

    public virtual void OnBrickDeath()
    {
        if (BrickDeath != null)
        {
            BrickDeath(this, new BrickEventArgs(numBricks, pointValue));
        }
    }

    bool powerupWillSpawn()
    {
        int randomInteger = UnityEngine.Random.Range(0, 101);
        if(randomInteger <= powerupPercentChance)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public delegate void PowerupSpawnAction(object source, PowerupEventargs args);
    public event PowerupSpawnAction PowerupSpawned;

    public virtual void OnPowerupSpawned(PowerupScript powerupScript)
    {
        if(PowerupSpawned != null)
        {
            PowerupSpawned(this, new PowerupEventargs(powerupScript));
        }
    }

    void SpawnPowerup()
    {
        if (powerupPrefab == null)
        {
            Debug.Log("Include the powerup prefab in the brick prefab");
            return;
        }

        Vector3 powerupPosition = transform.position;
        Quaternion powerupRotation = Quaternion.identity;

        GameObject powerup = Instantiate(powerupPrefab, powerupPosition, powerupRotation);
        PowerupScript powerupScript = powerup.GetComponent<PowerupScript>();
        OnPowerupSpawned(powerupScript);
    }
}

public class BrickEventArgs : EventArgs
{
    public int NumBricks { get; set; }
    public int Points { get; set; }
    public BrickEventArgs(int NumBricks, int Points)
    {
        this.NumBricks = NumBricks;
        this.Points = Points;
    }
}

public class PowerupEventargs : EventArgs
{
    public PowerupScript powerupScript { get; set; }
    public PowerupEventargs(PowerupScript powerupScript)
    {
        this.powerupScript = powerupScript;
    }
}
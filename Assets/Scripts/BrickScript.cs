using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickScript : MonoBehaviour {

    static int numBricks = 0;
    public int pointValue = 1;
    public int hitPoints = 1;
    GameObject powerup = null;

    public GameObject powerupPrefab;
    public int powerupPercentChance;

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
        } else
        {
            if (powerupWillSpawn())
            {
                SpawnPowerup();
            }
        }
    }

    bool powerupWillSpawn()
    {
        int randomInteger = Random.Range(0, 101);
        if(randomInteger <= powerupPercentChance)
        {
            return true;
        } else
        {
            return false;
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

        Instantiate(powerupPrefab, powerupPosition, powerupRotation);
    }
}

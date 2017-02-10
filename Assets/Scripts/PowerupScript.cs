using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

    public Material extraBallPowerupMaterial;

    PaddleScript paddleScript;

	// Use this for initialization
	void Start () {
        GameObject paddleObject = GameObject.Find("Paddle");
        paddleScript = paddleObject.GetComponent<PaddleScript>();
        ApplyMaterial();
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody>().AddTorque( Vector3.forward * 10f );	
	}

    private void OnCollisionEnter(Collision collision)
    {
        paddleScript.SpawnBall();
        Destroy(gameObject);
    }

    void ApplyMaterial()
    {
        Renderer powerupRenderer = gameObject.GetComponent<Renderer>();
        powerupRenderer.material = extraBallPowerupMaterial;
    }
}

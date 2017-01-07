using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    public float paddleSpeed;
    public GameObject ballPrefab;
    GameObject attachedBall = null;

	// Use this for initialization
	void Start () {
        SpawnBall();
	}
	
	// Update is called once per frame
	void Update () {

        //Left-right motion
        transform.Translate(paddleSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);

        if (Input.GetButtonDown("Jump"))
        {
            if (attachedBall)
            {
                attachedBall.GetComponent<Rigidbody>().AddForce(100f, 300f, 0);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            if(contact.thisCollider == GetComponent<Collider>())
            {
                float english = contact.point.x - transform.position.x;
                contact.otherCollider.attachedRigidbody.AddForce(300f * english, 0, 0);
            }
        }
    }

    void SpawnBall()
    {
        if(ballPrefab == null)
        {
            Debug.Log("Include the ball prefab in the paddle object.");
            return;
        }

        Vector3 ballPosition = transform.position + new Vector3(0, 1f, 0);
        Quaternion ballRotation = Quaternion.identity;

        attachedBall = (GameObject)Instantiate(ballPrefab, ballPosition, ballRotation);
    }
}

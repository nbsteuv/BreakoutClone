using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour {

    public float paddleSpeed;
    public GameObject attachedBall = null;
    public float autoOffset = 0;

    public float clamp = 7.4f;

    bool auto = false;
    GameObject followingBall = null;
	
	void Update () {

        float newPositionX = transform.position.x;

        if (auto)
        {
            if (followingBall == null)
            {
                GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
                if (balls.Length > 0)
                {
                    followingBall = balls[0];
                }
            }
            if (followingBall != null)
            {
                newPositionX = Mathf.Clamp(followingBall.transform.position.x + autoOffset, -clamp, clamp);
            }
            
        } else
        {
            newPositionX = Mathf.Clamp(transform.position.x + paddleSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), -clamp, clamp);
        }
        

        transform.position = new Vector3 (newPositionX, transform.position.y, transform.position.z);

        if (attachedBall)
        {
            Rigidbody ballRidgidbody = attachedBall.GetComponent<Rigidbody>();
            ballRidgidbody.position = transform.position + new Vector3(0, 1f, 0);

            if (Input.GetButtonDown("Jump"))
            {
                ballRidgidbody.isKinematic = false;
                ballRidgidbody.AddForce(300f * Input.GetAxis("Horizontal"), 300f, 0);
                attachedBall = null;
            }
        }
    }

    public void toggleAuto()
    {
        auto = !auto;
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

}

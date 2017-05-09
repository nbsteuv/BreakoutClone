using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour {

    public float paddleSpeed;
    public GameObject attachedBall = null;

	void Start () {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("MusicManager"));
    }
	
	void Update () {

        //Left-right motion
        transform.Translate(paddleSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);

        if(transform.position.x > 7.4f)
        {
            transform.position = new Vector3(7.4f, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -7.4f)
        {
            transform.position = new Vector3(-7.4f, transform.position.y, transform.position.z);
        }

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

    public void EndGame()
    {
        GetComponent<MeshRenderer>().enabled = false;
        SceneManager.LoadScene("GameOver");
    }

}

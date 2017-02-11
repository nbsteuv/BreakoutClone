using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour {

    public float paddleSpeed;
    public GameObject ballPrefab;
    GameObject attachedBall = null;
    TextMesh livesText;

    int lives = 3;
    int score = 0;
    public GUISkin scoreboardSkin;

    int balls = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Lives Counter"));
        DontDestroyOnLoad(GameObject.Find("MusicManager"));
        livesText = GameObject.Find("Lives Counter").GetComponent<TextMesh>();
        livesText.text = "Lives: " + lives;
        SpawnBall();
    }
	
	// Update is called once per frame
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
            //More optimized by caching Rigidbody object
            Rigidbody ballRidgidbody = attachedBall.GetComponent<Rigidbody>();
            //TODO: Move the initial position vector to a public field
            ballRidgidbody.position = transform.position + new Vector3(0, 1f, 0);

            if (Input.GetButtonDown("Jump"))
            {
                ballRidgidbody.isKinematic = false;
                ballRidgidbody.AddForce(300f * Input.GetAxis("Horizontal"), 300f, 0);
                attachedBall = null;
            }
        }

    }

    public void LoseLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if (lives > 0)
        {
            SpawnBall();
        } else
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
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

    public void OnLevelWasLoaded(int level)
    {
        balls--;
        SpawnBall();
    }

    public void SpawnBall()
    {
        if(ballPrefab == null)
        {
            Debug.Log("Include the ball prefab in the paddle object.");
            return;
        }

        Vector3 ballPosition = transform.position + new Vector3(0, 1f, 0);
        Quaternion ballRotation = Quaternion.identity;

        attachedBall = (GameObject)Instantiate(ballPrefab, ballPosition, ballRotation);
        balls++;
    }

    void OnGUI()
    {
        GUI.skin = scoreboardSkin;
        GUI.Label(new Rect(0, 10, 300, 100), "Score: " + score);
    }

    public void AddPoint(int pointValue)
    {
        score += pointValue;
    }

    public void LoseBall()
    {
        Debug.Log("Lose ball called");
        balls--;
        Debug.Log(balls);
        if (balls <= 0)
        {
            LoseLife();
        }
    }

    public void AddLife()
    {
        lives++;
        livesText.text = "Lives: " + lives;
    }
}

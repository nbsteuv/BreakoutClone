﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour {

    public float paddleSpeed;
    public GameObject ballPrefab;
    public level[] levels;
    GameObject attachedBall = null;
    TextMesh livesText;

    int lives = 3;
    int score = 0;
    public GUISkin scoreboardSkin;
    bool win = false;

    int balls = 0;
    int currentLevel;
    Dictionary<int, string> levelDictionary = new Dictionary<int, string>();

	// Use this for initialization
	void Start () {
        currentLevel = 1;
        foreach(level level in levels)
        {
            levelDictionary.Add(level.levelNumber, level.levelName);
        }
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
            EndGame();
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
        GameObject gameOverText = GameObject.Find("Game Over Text");
        if (gameOverText)
        {
            string gameOver;
            if (win)
            {
                gameOver = "You Win!";
            }
            else
            {
                gameOver = "Game Over";
            }
           gameOverText.GetComponent<TextMesh>().text = gameOver;
            GameObject.Find("Score Text").GetComponent<TextMesh>().text = "Score:\n" + score;
        } else
        {
            balls = 0;
            Debug.Log("Balls: " + balls);
            SpawnBall();
        }
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

        if (!attachedBall)
        {
            attachedBall = (GameObject)Instantiate(ballPrefab, ballPosition, ballRotation);
            balls++;
            Debug.Log("Balls: " + balls);
        }
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
        balls--;
        Debug.Log("Balls: " + balls);
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

    public void WinLevel()
    {
        currentLevel++;
        if (levelDictionary.ContainsKey(currentLevel)){
            SceneManager.LoadScene(levelDictionary[currentLevel]);
        } else
        {
            win = true;
            EndGame();
        }
    }

    public void EndGame()
    {
        GetComponent<MeshRenderer>().enabled = false;
        SceneManager.LoadScene("GameOver");
    }

    [System.Serializable]
    public struct level
    {
        public int levelNumber;
        public string levelName;
    }
}

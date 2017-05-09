using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    static GameScript instance;

    public GameObject ballPrefab;
    public int lives = 3;

    GameObject paddle;
    Text scoreText;
    Text livesText;
    BrickScript[] brickScripts;

    int score = 0;
    int balls;

    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        } else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

    }

    void Start () {

    }
	
	void Update () {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        balls = 0;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("ScoreTextUI");
        if (scoreTexts.Length > 0)
        {
            scoreText = GameObject.FindGameObjectsWithTag("ScoreTextUI")[0].GetComponent<Text>();
            scoreText.text = "Score " + score;
        }

        GameObject[] livesTexts = GameObject.FindGameObjectsWithTag("LivesTextUI");
        if (livesTexts.Length > 0)
        {
            livesText = GameObject.FindGameObjectsWithTag("LivesTextUI")[0].GetComponent<Text>();
            livesText.text = lives + " Lives";
        }

        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        if(paddles.Length > 0)
        {
            paddle = paddles[0];
            SpawnBall();
        }

        brickScripts = GameObject.FindObjectsOfType<BrickScript>();
        foreach(BrickScript brickScript in brickScripts)
        {
            brickScript.BrickDeath += OnBrickDeath;
        }
    }

    public void SpawnBall()
    {
        if (ballPrefab == null)
        {
            Debug.Log("Include the ball prefab in the game controller object.");
            return;
        }

        if (paddle == null)
        {
            Debug.Log("No paddle found.");
            return;
        }
        Vector3 ballPosition = paddle.transform.position + new Vector3(0, 1f, 0);
        Quaternion ballRotation = Quaternion.identity;

        if (paddle.GetComponent<PaddleScript>().attachedBall == null)
        {
            paddle.GetComponent<PaddleScript>().attachedBall = (GameObject)Instantiate(ballPrefab, ballPosition, ballRotation);
            paddle.GetComponent<PaddleScript>().attachedBall.GetComponent<BallScript>().BallDeath += LoseBall;
            balls++;
        }
    }

    void LoseBall()
    {
        balls--;
        if (balls <= 0)
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = lives + " Lives";
        if (lives > 0)
        {
            SpawnBall();
        }
        else
        {
            //EndGame();
            Debug.Log("Lose.");
        }
    }

    public void OnBrickDeath(object source, BrickEventArgs args)
    {
        AddPoint(args.Points);
        if(args.NumBricks <= 0)
        {
            Debug.Log("Win.");
        }
    }

    void AddPoint(int points)
    {
        score += points;
        scoreText.text = "Score " + score;
    }

    void Lose()
    {

    }


}

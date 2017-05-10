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
    List<BallScript> ballScripts;
    List<PowerupScript> powerupScripts;
    Text endScoreText;

    int score = 0;
    int balls;

    int endScore = 0;

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
        ballScripts = new List<BallScript>();
        powerupScripts = new List<PowerupScript>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            BrickScript.numBricks = 0;
            LoadNextLevel();
        }
    }

    public void RegisterAction(Event action)
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;

        //Is this enough to prevent memory leaks and errors, or does this need to happen when ball is destroyed?
        foreach(BallScript ballScript in ballScripts)
        {
            ballScript.BallDeath -= LoseBall;
        }

        foreach (BrickScript brickScript in brickScripts)
        {
            brickScript.BrickDeath -= OnBrickDeath;
        }

        foreach (PowerupScript powerupScript in powerupScripts)
        {
            powerupScript.AddLife -= AddLife;
            powerupScript.SpawnExtraBall -= SpawnBall;
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        balls = 0;
        lives = 3;
        endScore = score;

        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("ScoreTextUI");
        if (scoreTexts.Length > 0)
        {
            scoreText = scoreTexts[0].GetComponent<Text>();
            scoreText.text = "Score " + score;
        }

        GameObject[] livesTexts = GameObject.FindGameObjectsWithTag("LivesTextUI");
        if (livesTexts.Length > 0)
        {
            livesText = livesTexts[0].GetComponent<Text>();
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
            brickScript.PowerupSpawned += OnPowerupSpawned;
        }

        GameObject[] endScoreTexts = GameObject.FindGameObjectsWithTag("ScoreText");
        if(endScoreTexts.Length > 0)
        {
            endScoreText = endScoreTexts[0].GetComponent<Text>();
            endScoreText.text = endScore + " Points";
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
            ballScripts.Add(paddle.GetComponent<PaddleScript>().attachedBall.GetComponent<BallScript>());
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
            Lose();
        }
    }

    public void AddLife()
    {
        lives++;
        livesText.text = lives + " Lives";
    }

    public void OnBrickDeath(object source, BrickEventArgs args)
    {
        AddPoint(args.Points);
        if(args.NumBricks <= 0)
        {
            LoadNextLevel();
        }
    }

    public void OnPowerupSpawned(object source, PowerupEventargs args)
    {
        args.powerupScript.AddLife += AddLife;
        args.powerupScript.SpawnExtraBall += SpawnBall;

    }

    void AddPoint(int points)
    {
        score += points;
        scoreText.text = "Score " + score;
    }

    void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    void Lose()
    {
        SceneManager.LoadScene("Lose");
    }


}

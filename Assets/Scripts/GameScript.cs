using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

    static GameScript instance;

    public GameObject ballPrefab;

    GameObject paddle;

    int balls;

    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject); ;
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
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        if(paddles.Length > 0)
        {
            paddle = paddles[0];
            SpawnBall();
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
            paddle.GetComponent<PaddleScript>().LoseLife();
        }
    }

    void Lose()
    {

    }


}

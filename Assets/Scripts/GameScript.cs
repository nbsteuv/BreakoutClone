using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

    static GameScript instance;
    GameObject paddle;

    int lives;
    int score;
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
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
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        if(paddles.Length > 0)
        {
            paddle = paddles[0];
        }
        
    }


}

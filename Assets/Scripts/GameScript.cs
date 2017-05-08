using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

    static GameScript instance;
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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

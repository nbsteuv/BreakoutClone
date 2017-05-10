using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour {

    static MusicManagerScript instance;

    public AudioClip[] songs;
    AudioSource currentAudio;

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
        currentAudio = GetComponent<AudioSource>();
	}
	
	void Update () {
		if(currentAudio.isPlaying == false)
        {
            currentAudio.clip = songs[Random.Range(0, songs.Length)];
            currentAudio.Play();
        }
	}
}

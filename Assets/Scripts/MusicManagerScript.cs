using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour {

    public AudioClip[] songs;
    AudioSource currentAudio;

	// Use this for initialization
	void Start () {
        currentAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentAudio.isPlaying == false)
        {
            currentAudio.clip = songs[Random.Range(0, songs.Length)];
            currentAudio.Play();
        }
	}
}

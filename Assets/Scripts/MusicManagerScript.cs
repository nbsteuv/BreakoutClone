using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour {

    public AudioClip[] songs;
    AudioSource currentAudio;

	// Use this for initialization
	void Start () {
        currentAudio = GetComponent<AudioSource>();
        currentAudio.clip = songs[0];
        currentAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public AudioClip[] blipAudio;

    public delegate void BallDeathAction();
    public event BallDeathAction BallDeath;

	void Start () {

	}
	
	void Update () {

    }

    public void Die()
    {
        OnBallDeath();
        Destroy(gameObject);
    }

    public virtual void OnBallDeath()
    {
        if (BallDeath != null)
        {
            BallDeath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(blipAudio[Random.Range(0, blipAudio.Length)], transform.position, 0.5f);
    }
}

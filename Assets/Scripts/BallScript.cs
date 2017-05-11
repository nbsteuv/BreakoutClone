using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public AudioClip[] blipAudio;
    public float downwardForce = 1f;
    public float rightwardForce = 1f;

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
        Debug.Log(collision.collider);
        if (!collision.collider.CompareTag("Paddle"))
        {
            GetComponent<Rigidbody>().AddForce(rightwardForce, -downwardForce, 0);
        }
        
    }
}

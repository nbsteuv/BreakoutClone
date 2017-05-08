using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public AudioClip[] blipAudio;

    public delegate void BallDeathAction();
    public event BallDeathAction BallDeath;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //Fire the ball
        //if (Input.GetButtonDown("Jump"))
        //{
        //    GetComponent<Rigidbody>().AddForce(100f, 300f, 0);
        //}
    }

    public void Die()
    {
        OnBallDeath();
        Destroy(gameObject);
        
        //GameObject paddleObject = GameObject.Find("Paddle");
        //PaddleScript paddleScript = paddleObject.GetComponent<PaddleScript>();
        //paddleScript.LoseBall();
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

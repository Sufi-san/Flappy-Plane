using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public LogicScript logic;
    public AudioSource flapSound;
    public bool planeNotCrashed = true;
    public float flightStrength = 38;
    public int boundTop = 63;
    public int boundBottom = -57;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentPositionY = transform.position.y; 

        if(currentPositionY > boundTop)
        {
            myRigidbody.velocity = Vector2.up * flightStrength;
            initiateGameOver();
        }

        if(currentPositionY < boundBottom)
        {
            initiateGameOver();
        }

        if(Input.GetKeyDown(KeyCode.Space) && planeNotCrashed)
        {
            flapSound.Play();
            myRigidbody.velocity = Vector2.up * flightStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
       initiateGameOver();
    }

    private void initiateGameOver() 
    {
        Debug.Log("Plane Collided!!");
        AudioSource gameOverSound = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        if(planeNotCrashed) gameOverSound.Play();
        logic.gameOver();
        planeNotCrashed = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonPlayerController : MonoBehaviour {
    private Animator animator;
    public float speed;             //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public Text channelText;
    private int connectStatus = 0;
    private int NONE = 0;
    private int SARA = 1;
    private int ALEX = 2;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        if (Input.GetKeyDown("left"))
        {
            animator.SetBool("rightTurn", false);
            animator.SetBool("leftTurn", true);
        }
        else if (Input.GetKeyDown("right"))
        {
            animator.SetBool("leftTurn", false);
            animator.SetBool("rightTurn",true);
        }
        rb2d.AddForce(movement * speed);
               
    }
    void MoveControl()
    {
        float moveX = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.Translate(moveX, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sara")
        {

        }
        else if(collision.gameObject.tag == "Alex")
        {

        }

    }
    public void setConnectedStatus(int stat)
    {
        connectStatus = stat;
        //Debug.Log("Now status : " + stat);
    }
    private void ChannelTextControl()
    {

    }
    
    
}

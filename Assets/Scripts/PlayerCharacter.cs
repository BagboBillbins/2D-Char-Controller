using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    public float maxSpeed = 10;
    public float jumpForce = 700f;
    float groundRadius = 0.2f;
    bool facingRight = true;
    bool grounded = false;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    Animator anim;
    Rigidbody2D rigBod;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        rigBod = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground0", grounded);
        anim.SetFloat("vSpeed", rigBod.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigBod.velocity = new Vector2(move * maxSpeed, rigBod.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

	}

    void Update()
    {
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Grounded", false);
            rigBod.AddForce(new Vector2(0, jumpForce));

        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}

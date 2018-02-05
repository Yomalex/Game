using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Transform groundCheck;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;

    private Animator ani;
    private Rigidbody2D rb2D;

    private bool jump = false;
    private bool grounded = false;
    private bool dirRight = true;

	// Use this for initialization
	void Awake () {
        ani = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.Linecast(
            transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if(Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
	}

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        ani.SetFloat("Speed", Mathf.Abs(h));

        rb2D.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2D.velocity.x) > maxSpeed)
            rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);

        if (h > 0 && !dirRight)
            Flip();
        else if (h < 0 && dirRight)
            Flip();

        if(jump)
        {
            jump = false;

            ani.SetTrigger("Jump");

            rb2D.AddForce(Vector2.up * jumpForce);
        }
    }

    void Flip()
    {
        dirRight = !dirRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

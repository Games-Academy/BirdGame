using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Force = 1f;
    public float JumpForce = 1f;
    public float MaxSpeed = 2f;
    public LayerMask CharacterLayer;
    private float horizontal;
    private Rigidbody2D rigidbody;

    void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //MovementClamped();
        //MovementSimple();
        Flip();
        Jump();
    }

    private void Movement()
    {
        this.horizontal = Input.GetAxis("Horizontal");
        rigidbody.AddForce(Vector2.right * this.Force * horizontal * Time.deltaTime);
    }

    public void MovementClamped()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -this.MaxSpeed, this.MaxSpeed);
        rigidbody.velocity = velocity;
    }

    private void MovementSimple()
    {
        //Add to position to move character
        this.transform.position += new Vector3(this.MaxSpeed, 0f, 0f) * horizontal * Time.deltaTime;
    }

    /// <summary>
    /// Flips the character sprite if the character moves in another direction
    /// </summary>
    private void Flip()
    {
        Vector3 scale = this.transform.localScale;
        if (this.horizontal < 0f)
        {
            scale.x = -1.2f;
        }
        if (this.horizontal > 0f)
        {
            scale.x = 1.2f;
        }
        this.transform.localScale = scale;
    }

    private void Animation()
    {
        Animator animator = this.GetComponent<Animator>();
        float speed = Mathf.Abs(this.rigidbody.velocity.x);
        animator.SetFloat("Speed", speed);
    }

    private void Jump()
    {
        bool grounded = Physics2D.Raycast(this.transform.position, Vector2.down, 1f, ~this.CharacterLayer);
        Debug.Log(grounded);
        bool jump = Input.GetButtonDown("Jump");
        if (jump && grounded)
        {
            rigidbody.AddForce(Vector2.up * this.JumpForce);
        }
    }
}

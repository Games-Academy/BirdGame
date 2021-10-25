using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Force = 1f;
    public float JumpForce = 1f;
    public float MaxSpeed = 2f;
    public LayerMask CharacterLayer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float horizontal = Input.GetAxis("Horizontal");
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.right * this.Force * horizontal * Time.deltaTime);

        //Flip
        Vector3 scale = this.transform.localScale;
        if (horizontal < 0f)
        {
            scale.x = -1.2f;
        }
        if(horizontal > 0f)
        {
            scale.x = 1.2f;
        }
        this.transform.localScale = scale;

        //Animation
        Animator animator = this.GetComponent<Animator>();
        float speed = Mathf.Abs(rigidbody.velocity.x);
        animator.SetFloat("Speed", speed);

        //Jump
        bool grounded = Physics2D.Raycast(this.transform.position, Vector2.down,1f,~this.CharacterLayer);
        Debug.Log(grounded);
        bool jump = Input.GetButtonDown("Jump");
        if(jump && grounded)
        {
            rigidbody.AddForce(Vector2.up * this.JumpForce);
        }

        // Vector2 velocity = rigidbody.velocity;
        // velocity.x = Mathf.Clamp(velocity.x, -this.MaxSpeed, this.MaxSpeed);
        // rigidbody.velocity = velocity;

        //Debug.Log(velocity);

        //Add to position to move character
        //this.transform.position += new Vector3(this.Speed,0f,0f) * horizontal * Time.deltaTime;
    }
}

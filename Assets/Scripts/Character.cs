using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int PlayerIndex = 0;
    public float Force = 1f;
    public float JumpForce = 1f;
    public float MaxSpeed = 2f;

    [Range(0f,1f)]
    public float ReverseFactor = 1f;
    public LayerMask CharacterLayer;
    private float horizontal;
    private new Rigidbody2D rigidbody;
    private Weapon weapon;

    void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.weapon = this.GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        this.horizontal = Input.GetAxis("Horizontal_"+this.PlayerIndex);
        //MoveByForce();
        //MoveClamped();
        //MoveSimple();
        MoveAdvanced();
        Flip();
        Jump();

        if(Input.GetButtonDown("Fire_"+this.PlayerIndex)) this.weapon.Fire();
    }

    private void MoveByForce()
    {
        rigidbody.AddForce(Vector2.right * this.Force * horizontal * Time.deltaTime);
    }

    /// <summary>
    /// Moves the character by force which gets lowered when reaching maximum speed
    /// </summary>
    private void MoveAdvanced()
    {
        float velocity = Mathf.Abs(this.rigidbody.velocity.x);
        float factor = Mathf.Clamp01(velocity / this.MaxSpeed);
        float inverseFactor = 1f - factor;
        if(this.horizontal * this.rigidbody.velocity.x < 0f) inverseFactor = this.ReverseFactor;
        rigidbody.AddForce(Vector2.right * this.Force * inverseFactor * horizontal * Time.deltaTime);
    }

    public void MoveClamped()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -this.MaxSpeed, this.MaxSpeed);
        rigidbody.velocity = velocity;
    }

    private void MoveSimple()
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

    /// <summary>
    /// Sets animation parameters
    /// </summary>
    private void Animation()
    {
        Animator animator = this.GetComponent<Animator>();
        float speed = Mathf.Abs(this.rigidbody.velocity.x);
        animator.SetFloat("Speed", speed);
    }

    /// <summary>
    /// Adds vertical jump force when pressing jump button and being grounded
    /// </summary>
    private void Jump()
    {
        bool grounded = Physics2D.Raycast(this.transform.position, Vector2.down, 1f, ~this.CharacterLayer);
        Debug.Log(grounded);
        bool jump = Input.GetButtonDown("Jump_"+this.PlayerIndex);
        if (jump && grounded)
        {
            rigidbody.AddForce(Vector2.up * this.JumpForce);
        }
    }
}

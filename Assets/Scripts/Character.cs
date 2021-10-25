using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Force = 1f;
    public float MaxSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.right * this.Force * horizontal * Time.deltaTime);

        // Vector2 velocity = rigidbody.velocity;
        // velocity.x = Mathf.Clamp(velocity.x, -this.MaxSpeed, this.MaxSpeed);
        // rigidbody.velocity = velocity;

        //Debug.Log(velocity);

        //Add to position to move character
        //this.transform.position += new Vector3(this.Speed,0f,0f) * horizontal * Time.deltaTime;
    }
}

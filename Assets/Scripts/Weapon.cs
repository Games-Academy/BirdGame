using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletSpeed = 1f;
    public float FireRate = 1f;
    private float lastFired = 0f;

    void Update()
    {
        float delay = 1f / this.FireRate;
        bool canShoot = this.lastFired + delay <= Time.time;
        
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            this.lastFired = Time.time;

            if (this.transform.lossyScale.x > 0f)
            {
                GameObject bullet = Instantiate(this.Bullet, this.transform.position, Quaternion.identity);
                var rigidbody = bullet.GetComponent<Rigidbody2D>();
                rigidbody.velocity = Vector2.right * this.BulletSpeed;
            }
            else
            {
                GameObject bullet = Instantiate(this.Bullet, this.transform.position, Quaternion.Euler(0f,0f,180f));
                var rigidbody = bullet.GetComponent<Rigidbody2D>();
                rigidbody.velocity = Vector2.left * this.BulletSpeed;
            }

            //3) Play shoot sound
            var audioSource = this.GetComponent<AudioSource>();
            audioSource.Play();
        }
    }
}

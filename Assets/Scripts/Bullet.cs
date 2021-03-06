using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Explosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
        Instantiate(this.Explosion, this.transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(this.Bullet,this.transform.position,Quaternion.identity);
        }
    }
}

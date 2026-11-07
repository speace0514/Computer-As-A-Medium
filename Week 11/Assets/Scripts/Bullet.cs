using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 25f;
    public float bulletKnockback = 0.1f;
    public float bulletSpeed = 1f;
    public float bulletDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(int newDirection, float newDamage, float newSpeed, float newKnockback)
    {
        bulletDirection = newDirection;
        bulletDamage = newDamage;
        bulletSpeed = newSpeed;
        bulletKnockback = newKnockback;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, bulletDirection, 0) * Time.deltaTime * bulletSpeed);
        if (Math.Abs(transform.position.y) > 6.5f || Math.Abs(transform.position.x) > 11f) 
        {
            Destroy(this.gameObject);
        }
    }
}

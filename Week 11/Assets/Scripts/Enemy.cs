using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 100f;
    public float enemyVerticalSpeed = 1f;
    public float enemyKnockBack = 1f;
    public int enemyAttackSpeed = 60;
    public float enemyBulletSpeed = 4f;
    public float enemyBulletDamage = 25f;

    private float attackTimer = 60f;
    private GameObject spawnedBullet;

    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        attackTimer = enemyAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -enemyVerticalSpeed, 0) * Time.deltaTime * 3f);
        if (transform.position.y < -6.5f)
        {
            Debug.Log("I destroyed");
            Destroy(this.gameObject);
        }
        if (enemyHealth <= 0f)
        {
            Debug.Log("I die");
            Destroy(this.gameObject);
        }
        Shooting();
    }
    void Shooting()
    {
        Debug.Log(attackTimer);
        attackTimer -= Time.deltaTime * 60f;
        if (attackTimer <= 0)
        {
            //Creates bullet
            spawnedBullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
            //Finds the Bullet.cs and attaches its public components to variable named bulletScript
            Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();
            //Passes variables to the new bullet
            bulletScript.Initialize(-1, enemyBulletDamage, enemyBulletSpeed, 0f);
            spawnedBullet.tag = "EnemyBullet"; 

            attackTimer = enemyAttackSpeed;
        }
    }
    void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("PlayerBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                enemyHealth -= bullet.bulletDamage;
                transform.Translate(new Vector3(0, enemyKnockBack * bullet.bulletKnockback, 0));
                Destroy(other.gameObject);
            }
        }
    }  
}

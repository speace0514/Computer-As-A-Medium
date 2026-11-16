using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;
    public bool isEnemy;
    public bool hasDespawn;
    public float timer; 

    
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDespawn == true)
        {
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
       
        if (isEnemy == true)
        {
            if (whatDidIHit.tag == "Player")
            {
                whatDidIHit.GetComponent<PlayerController>().LoseALife();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else if (whatDidIHit.tag == "Weapons")
            {
                Destroy(whatDidIHit.gameObject);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                gameManager.AddScore(5);
                Destroy(this.gameObject);
            }
        }
        else
        {
            gameManager.AddScore(1);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;
    public bool isEnemy = true;
    public bool hasDespawn;
    public float timer; 

    
    private GameManager gameManager;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
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
                if (whatDidIHit.GetComponent<PlayerController>().invincible == false)
                {
                    whatDidIHit.GetComponent<PlayerController>().LoseALife();
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                    gameManager.PlaySound(2);
                    Destroy(this.gameObject);
                }
            }
            else if (whatDidIHit.tag == "Weapons")
            {
                Destroy(whatDidIHit.gameObject);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                gameManager.AddScore(5);
                gameManager.PlaySound(2);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (this.gameObject.tag == "Coin" && (whatDidIHit.tag == "Player" || whatDidIHit.tag == "Weapons"))
            {
                gameManager.AddScore(1);
                Destroy(this.gameObject);
                if (whatDidIHit.tag == "Weapons")
                {
                    Destroy(whatDidIHit.gameObject);
                }
            }
            else if (this.gameObject.tag == "Powerup" && (whatDidIHit.tag == "Player" || whatDidIHit.tag == "Weapons"))
            {
                player.PowerUp();
                Destroy(this.gameObject);
                if (whatDidIHit.tag == "Weapons")
                {
                    Destroy(whatDidIHit.gameObject);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;
    private bool invincible = false;
    private bool hasShield = false;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject shieldPrefab;
    private int whichPowerup = 4;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        gameManager.ChangeLivesText(lives);
        shieldPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        Debug.Log(hasShield);
    }

    public void LoseALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        if (invincible)
        {
            return;
        }
        if (hasShield)
        {
            hasShield = false;
            shieldPrefab.SetActive(false);
            return;

        }
        lives--;
        gameManager.ChangeLivesText(lives);
        invincible = true;
        StartCoroutine(InvincibleOff());
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    IEnumerator InvincibleOff()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(4f);
        GetComponent<SpriteRenderer>().color = Color.white;
        invincible = false;
    }
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Powerup")
        {
            switch (whichPowerup)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    if (hasShield == false)
                    {
                        hasShield = true;
                        shieldPrefab.SetActive(true);
                    }
                    break;
            }
        }
    }
}

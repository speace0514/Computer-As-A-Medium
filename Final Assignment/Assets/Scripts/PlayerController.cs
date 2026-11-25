using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;
    public bool invincible = false;
    private bool hasShield = false;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject shieldPrefab;
    private int whichPowerup = 4;
    [SerializeField] private float verticalScreenLimitUp;
    [SerializeField] private float verticalScreenLimitDown;

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
    }

    public void LoseALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        if (hasShield)
        {
            hasShield = false;
            shieldPrefab.SetActive(false);
            gameManager.PlaySound(1);
            return;

        }
        lives--;
        gameManager.ChangeLivesText(lives);
        invincible = true;
        StartCoroutine(InvincibleOff());
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.PlaySound(2);
            Destroy(this.gameObject);
        }
    }
    IEnumerator InvincibleOff()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(2f);
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
        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;
        horizontalInput = Input.GetAxis("Horizontal") * 1.25f;
        verticalInput = Input.GetAxis("Vertical") * .75f;
        //Move the player
        if (transform.position.y <= verticalScreenLimitUp && transform.position.y >= -verticalScreenLimitDown)
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        //If the player runs against a vertical boundary, they move 0 past the boundary and move regular speed back towards the play area
        else if (transform.position.y > verticalScreenLimitUp)
            transform.Translate(new Vector3(horizontalInput, verticalInput * .5f - Math.Abs(verticalInput) * .5f, 0) * Time.deltaTime * speed);
        else if (transform.position.y < -verticalScreenLimitDown)
            transform.Translate(new Vector3(horizontalInput, verticalInput * .5f + Math.Abs(verticalInput) * .5f, 0) * Time.deltaTime * speed);
        //Player leaves the screen horizontally
        if (Math.Abs(transform.position.x) >= horizontalScreenSize + 1)
            transform.Translate(new Vector3(transform.position.x * -2, 0, 0));

    }
    public void PowerUp()
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
                    gameManager.PlaySound(0);
                }
                break;
        }
        
    }
}

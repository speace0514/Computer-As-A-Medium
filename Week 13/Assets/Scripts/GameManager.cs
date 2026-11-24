using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds;

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;
    public GameObject powerupPrefab;
    

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        scoreText.text = "Score: " + score;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("CreateCoin", 1, 3);
        InvokeRepeating("CreatePowerup", 4, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundNumber)
    {
        if (soundNumber < 0 || soundNumber >= sounds.Length)
            return;

        audioSource.PlayOneShot(sounds[soundNumber]);
    }

    void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }
    void CreateCoin()
    {
        GameObject coin = Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, Random.Range(-verticalScreenSize, verticalScreenSize) * 0.9f, 0), Quaternion.Euler(0, 0, 0));
    }
    void CreatePowerup()
    {
        GameObject powerup = Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, Random.Range(-verticalScreenSize, verticalScreenSize) * 0.9f, 0), Quaternion.Euler(0, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
}


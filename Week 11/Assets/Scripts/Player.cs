using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //how to define a variable
    //1. access modifier: public or private
    //2. data type: int, float, bool, string
    //3. variable name: camelCase
    //4. value: optional

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    [SerializeField] private float horizontalScreenLimit;
    [SerializeField] private float verticalScreenLimitUp;
    [SerializeField] private float verticalScreenLimitDown;

    public GameObject bulletPrefab;

    void Start()
    {
        playerSpeed = 6f;
        rb = GetComponent<Rigidbody>();
        //This function is called at the start of the game

    }

    void Update()
    {
        //This function is called every frame; 60 frames/second
        Movement();
        Shooting();

    }

    void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal") * 1.5f;
        verticalInput = Input.GetAxis("Vertical") * .75f;
        //Move the player
        if (transform.position.y <= verticalScreenLimitUp && transform.position.y >= -verticalScreenLimitDown)
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);
        //If the player runs against a vertical boundary, they move 0 past the boundary and move regular speed back towards the play area
        else if(transform.position.y > verticalScreenLimitUp)
            transform.Translate(new Vector3(horizontalInput, verticalInput * .5f - Math.Abs(verticalInput) * .5f, 0) * Time.deltaTime * playerSpeed);
        else if (transform.position.y < -verticalScreenLimitDown)
            transform.Translate(new Vector3(horizontalInput, verticalInput * .5f + Math.Abs(verticalInput) * .5f, 0) * Time.deltaTime * playerSpeed);
        //Player leaves the screen horizontally
        if (Math.Abs(transform.position.x) > horizontalScreenLimit)
            transform.Translate(new Vector3(transform.position.x * -2, 0, 0));

    }

}

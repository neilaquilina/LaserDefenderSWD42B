using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR.WSA.Input;

public class Player : MonoBehaviour
{
    //makes the variable editable from Unity Editor
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.7f;

    [SerializeField] GameObject laserPrefab;
    
    [SerializeField] float laserFiringSpeed = 0.2f;

    float xMin, xMax, yMin, yMax;
    
    Coroutine fireCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        StartCoroutine(PrintAndWait());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();

        
    }

    //coroutine to print 2 messages
    private IEnumerator PrintAndWait()
    {
        print("Message 1");
        yield return new WaitForSeconds(10);
        print("Message 2 after 10 seconds");
    }

    //coroutine to fire continuously
    private IEnumerator FireContinuously()
    {
        while (true) //while coroutine is running
        {
            //create an instance of laser
            //at the ship's position
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            //give a velocity to the laser in y-axis of 15
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 15f);

            //wait laserFiringSpeed before firing again
            yield return new WaitForSeconds(laserFiringSpeed);

        }
    }


    private void Fire()
    {
       //if I press fire button
       if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }

       //if I release fire button
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void SetUpMoveBoundaries()
    {
        //get the Main Camera from Unity
        Camera gameCamera = Camera.main;

        //set the boundaries on the x-axis
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        //set the boundaries on the y-axis
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    //moves the Player ship
    private void Move()
    {
        //var changes its variable type
        //depending on what I save in it
        //deltaX will have the difference in the x-axis which the Player moves
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //newXPos   = current x-position   + difference in x
        var newXPos = transform.position.x + deltaX;
        //apply boundaries on x-axis
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        //the above in y axis:
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        //move the Player ship to the newXPos
        this.transform.position = new Vector2(newXPos, newYPos);

        
    }
}

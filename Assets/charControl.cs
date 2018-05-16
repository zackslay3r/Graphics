using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charControl : MonoBehaviour {


    public float velocity = 5;
    public float turnSpeed = 10;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    //Transform cam;
    Rigidbody rBody;
    public Camera playercam;
    Animator zombie;

    Vector3 vec;


	// Use this for initialization
	void Start () {
        //cam = playercam.transform;
        rBody = GetComponent<Rigidbody>();
        zombie = GetComponent<Animator>();
        //zombie.SetBool("Running", false);
        //zombie.SetBool("Idle", true);
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();

        // If there is no input.
        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1)
        {
            zombie.SetBool("isRunning", false);
            zombie.SetBool("isIdle", true);
            return;
        }
        CalculateDirection();
        Rotate();
        Move();

	}

     void FixedUpdate()
    {
      
    }

    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += playercam.transform.eulerAngles.y;
    }


    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void Move()
    {
        
        transform.position += transform.forward * velocity * Time.deltaTime;
        if (velocity > 0)
        {
            zombie.SetBool("isRunning", true);
            zombie.SetBool("isIdle", false);
        }
        else
        {
          
        }
    }


}

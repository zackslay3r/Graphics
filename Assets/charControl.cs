using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charControl : MonoBehaviour {

    public float gravity = 20;
    public float velocity = 5;
    public float turnSpeed = 10;
    public float jumpVel = 12;
    public float height = 0.5f;
    public float heightPadding = 0.05f;
    public LayerMask ground;
    public float maxGroundAngle = 120;
    public bool debug;
    public float oldY;

    private Vector3 moveDirection = Vector3.zero;

    Vector2 input;
    float jumpInput;
    float angle;
    float groundAngle;

    public string JUMP_AXIS = "Jump";

    Quaternion targetRotation;
    //Transform cam;
    Rigidbody rBody;
    CapsuleCollider cCollider;
    public Camera playercam;
    Animator zombie;

    Vector3 vec;

    Vector3 forward;
    RaycastHit hitInfo;
    public bool grounded;

    CharacterController charController;


    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    //public float gravity = 20.0F;
    //private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        zombie = GetComponent<Animator>();
    }

    void Update()
    {

        //if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1)
        //{
        //    zombie.SetBool("isRunning", false);
        //    zombie.SetBool("isIdle", true);
        //    return;
        //}
        //else
        //{
        //    zombie.SetBool("isRunning", true);
        //    zombie.SetBool("isIdle", false);
        //}
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    //// Use this for initialization
    //void Start () {
    //       //cam = playercam.transform;
    //       //rBody = GetComponent<Rigidbody>();

    //       cCollider = GetComponent<CapsuleCollider>();
    //       charController = GetComponent<CharacterController>();
    //       //zombie.SetBool("Running", false);
    //       //zombie.SetBool("Idle", true);
    //}

    //// Update is called once per frame
    //void Update () {
    //       GetInput();
    //       CalculateDirection();
    //       CalculateForward();
    //       CalculateGroundAngle();
    //       CheckGround();
    //       ApplyGravity();
    //       DrawDebugLines();


    //       // If there is no input.
    //       if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1)
    //       {
    //           zombie.SetBool("isRunning", false);
    //           zombie.SetBool("isIdle", true);
    //           return;
    //       }

    //       Rotate();
    //       //Move();
    //}

    //    private void FixedUpdate()
    //   {
    //       CharacterController controllerChar = GetComponent<CharacterController>();

    //       moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //       moveDirection = transform.TransformDirection(moveDirection);
    //       moveDirection.y = oldY;
    //       moveDirection *= velocity;
    //       zombie.SetBool("isRunning", true);
    //       zombie.SetBool("isIdle", false);
    //       if (grounded == true && Input.GetKeyDown(KeyCode.Space))
    //       {
    //           Jump();
    //           grounded = false;

    //       }
    //       oldY -= gravity * Time.deltaTime;

    //       controllerChar.Move(moveDirection * Time.deltaTime);
    //       //CheckGround();

    //   }

    //   void GetInput()
    //   {
    //       input.x = Input.GetAxisRaw("Horizontal");
    //       input.y = Input.GetAxisRaw("Vertical");
    //       jumpInput = Input.GetAxisRaw("Jump");
    //   }

    //   void CalculateDirection()
    //   {
    //       angle = Mathf.Atan2(input.x, input.y);
    //       angle = Mathf.Rad2Deg * angle;
    //       angle += playercam.transform.eulerAngles.y;
    //   }


    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    //   void Jump()
    //   {
    //       //if (grounded && Input.GetKeyDown(KeyCode.Space))
    //       //{

    //       //    //rBody.AddForce(Vector3.up * jumpVel, ForceMode.Impulse);
    //       //    rBody.velocity = new Vector3(rBody.velocity.x, jumpVel, rBody.velocity.z);

    //       //}
    //       //else if (!Input.GetKeyDown(KeyCode.Space) && grounded)
    //       //{
    //       //    rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, rBody.velocity.z);
    //       //}
    //       //else
    //       //{
    //           oldY = jumpVel;
    //       //}


    //   }

    //   void Move()
    //   {
    //       if(groundAngle >= maxGroundAngle)
    //       {
    //           return;
    //       }
    //       transform.position += forward * velocity * Time.deltaTime;
    //       if (velocity > 0)
    //       {
    //           zombie.SetBool("isRunning", true);
    //           zombie.SetBool("isIdle", false);
    //       }
    //       else
    //       {

    //       }
    //   }

    //   void CalculateForward()
    //   {
    //       if (!grounded)
    //       {
    //           forward = transform.forward;
    //           return;
    //       }
    //       forward = Vector3.Cross(transform.right, hitInfo.normal);
    //   }
    //   void CalculateGroundAngle()
    //   {
    //       if (!grounded)
    //       {
    //           groundAngle = 90;
    //           return;
    //       }

    //       groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    //   }

    //   void CheckGround()
    //   {
    //       if (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, height + heightPadding, ground))
    //       {
    //           if (Vector3.Distance(transform.position, hitInfo.point) < height)
    //           {
    //               transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * height, 5 * Time.deltaTime);
    //           }
    //               grounded = true;
    //       }
    //       else
    //       {
    //           grounded = false;
    //       }
    //   }

    //   void ApplyGravity()
    //   {
    //       if (!grounded)
    //       {
    //           transform.position += Physics.gravity * Time.deltaTime;
    //       }

    //   }

    //   void DrawDebugLines()
    //   {
    //       if (debug)
    //       {
    //           Debug.DrawLine(transform.position, transform.position + forward * height * 2, Color.blue);
    //           Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.green);
    //       }
    //   }
}

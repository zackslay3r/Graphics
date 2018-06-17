using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChararacterControll : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings
    {
    public float forwardVel = 12;
    public float rotateVel = 100;
    public float jumpVel = 25;
    public float distToGrounded = 0.8f;
    
    public LayerMask ground;
        
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string TURN_AXIS = "Horizontal";
        public string FORWARD_AXIS = "Vertical";
        
        public string JUMP_AXIS = "Jump";
    }

    void Grounded()
    {
        platformCount = 0;
        //bool grounded = 
        //Physics.Raycast(transform.position,Vector3.down,out hit,100.0f,moveSetting.ground);
        if (debug)
        {
            Vector3 debugDistance = Vector3.down * moveSetting.distToGrounded;
            Debug.DrawRay(transform.position, debugDistance, Color.red);
        }

        float distanceToPoints = collider.height / 2 - collider.radius;
        Vector3 p1 = transform.position + collider.center + Vector3.up * distanceToPoints;
        Vector3 p2 = transform.position + collider.center - Vector3.up * distanceToPoints;

        float castDistance = moveSetting.distToGrounded;

        float radius = collider.radius;

        

        RaycastHit[] hits = Physics.CapsuleCastAll(p1,p2,radius,Vector3.down,castDistance);
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.distance < moveSetting.distToGrounded && hit.transform.tag != "Player")
            {
                platformCount++;
            }

        }
        if (platformCount > 0)
        {
            anim.SetBool("onAir", false);
            //anim.SetBool("onAir", false);
            anim.SetBool("hitGround", true);
        }
        else
        {
                anim.SetBool("onAir", true);
                anim.SetBool("hitGround", false);
        }
        //if (hit.distance < moveSetting.distToGrounded)
        //{
        //    //anim.SetBool("onAir", false);
        //    anim.SetBool("onAir", false);
        //    anim.SetBool("hitGround", true);

        //    //return true;
        //}
        //else
        //{
        //    anim.SetBool("onAir", true);
        //    anim.SetBool("hitGround", false);

        //    //return false;
        //    //anim.SetBool("onAir", true);
        //}

    }
    private bool cooldown = false;
    public float height = 0.5f;
    public float heightPadding = 0.05f;
    public float maxGroundAngle = 120f;
    public bool debug;
    public int currentPlatformsTouching = 0;
    public int platformCount;
    float groundAngle;


    Vector3 forward;
    RaycastHit hitInfo;

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();
    Vector3 velocity = Vector3.zero;
    Quaternion targetRotation;
    Rigidbody rBody;
    CharacterController charController;
    CapsuleCollider collider;
    float forwardInput, turnInput, jumpInput;
    Animator anim;
   

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

	// Use this for initialization
	void Start () {
        targetRotation = transform.rotation;
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();

        if (GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("THe character needs a rigidbody.");
        }
        forwardInput = turnInput = jumpInput = 0;
	}

    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
        jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);

        // if forwardInput is negative, we want a different animation running.
        if (forwardInput < -0.05)
        {
            anim.SetBool("walkBackwards", true);
        }
        else
        {
            anim.SetBool("walkBackwards", false);
        }


        float animValue = Mathf.Abs(forwardInput) + Mathf.Abs(turnInput);


        anim.SetFloat("Forward", animValue, .1f, Time.deltaTime);
    }


	// Update is called once per frame
	void Update ()
    {
        GetInput();
        Turn();
        // want to check if we are grounded and if we are, then we want to make sure appropirate values are being set. 
        Grounded();


	}
    void OnCollisionEnter(Collision other)
    {
        //onGround = true;
        //rBody.drag = 5;
        //anim.SetBool("onAir", false);
        //anim.SetBool("hitGround", true);
      //  currentPlatformsTouching++;

        //    if(Grounded())
        //{
            
            //rBody.drag = 5;
            //anim.SetBool("onAir", false);
            //anim.SetBool("hitGround", true);
       //}
    }

    void OnCollisionExit(Collision other)
    {
        //currentPlatformsTouching--;

        
    }

    void FixedUpdate()
    {
        velocity.z -= 5f;
        if (velocity.z < 0)
        {
            velocity.z = 0;
        }

        Run();
        Jump();

        rBody.velocity = transform.TransformDirection(velocity);
    }

    void Run()
    {
        
        if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
        {
           


            //move
            //rBody.velocity = transform.forward * forwardInput * moveSetting.forwardVel;
            velocity.z = moveSetting.forwardVel * forwardInput;
        }
        else
        {
            // zero velocity
            //rBody.velocity = Vector3.zero;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (currentPlatformsTouching == 0)
        {

 
            //anim.SetBool("onAir", true);
           // anim.SetBool("hitGround", false);
        }
        if (jumpInput > 0 && anim.GetBool("hitGround") == true && cooldown == false)
        {
            velocity.y = moveSetting.jumpVel;

            Invoke("ResetCooldown", 2.0f);
            cooldown = true;
        }
        else if (jumpInput == 0 && anim.GetBool("hitGround") == true)
        {
            velocity.y = 0;

        }
        else
        {
            velocity.y -= physSetting.downAccel;
        }

        

        //// check the plaforms we are on. if we are on at least one and we are grounded...
        //if (currentPlatformsTouching >= 1 && Grounded())
        //{
        //    onGround = true;
        //    rBody.drag = 5;
        //    anim.SetBool("onAir", false);
        //    anim.SetBool("hitGround", true);
        //}
        //else
        //{
        //    onGround = false;
        //    rBody.drag = 5;
        //    anim.SetBool("onAir", true);
        //    anim.SetBool("hitGround", false);
        //}

    }

    void ResetCooldown()
    {
        cooldown = false;
    }
}

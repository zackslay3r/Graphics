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

    bool Grounded()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
        return grounded;
    }

    public float height = 0.5f;
    public float heightPadding = 0.05f;
    public float maxGroundAngle = 120f;
    public bool debug;

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
    float forwardInput, turnInput, jumpInput;
    Animator anim;
    public bool onGround;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

	// Use this for initialization
	void Start () {
        targetRotation = transform.rotation;
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

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

        float animValue = Mathf.Abs(forwardInput) + Mathf.Abs(turnInput);


        anim.SetFloat("Forward", animValue, .1f, Time.deltaTime);
    }


	// Update is called once per frame
	void Update ()
    {
        GetInput();
        Turn();
        
	}
    void OnCollisionEnter(Collision other)
    {
        onGround = true;
        rBody.drag = 5;
        anim.SetBool("onAir", false);
        anim.SetTrigger("HitGround");
    }

    void OnCollisionExit(Collision other)
    {
        onGround = false;
        rBody.drag = 5;
        anim.SetBool("onAir", true);
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
        if (jumpInput > 0 && onGround)
        {
            velocity.y = moveSetting.jumpVel;
        }
        else if (jumpInput == 0 && onGround)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= physSetting.downAccel;
        }
    }
}

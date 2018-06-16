using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    Rigidbody rigidBody;
    Animator anim;
    CapsuleCollider capCol;
    [SerializeField] PhysicMaterial zfriction;
    [SerializeField] PhysicMaterial mfriction;
    Transform cam;

    [SerializeField] float speed = 4;
    [SerializeField] float turnSpeed = 5;
    [SerializeField] float jumpPower = 1;

    Vector3 directionPos;
    Vector3 lookPos;
    Vector3 storeDir;

    float horizontal;
    float vertical;

    bool jumpInput;
    bool onGround;


	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        capCol = GetComponent<CapsuleCollider>();
        SetupAnimator();
	}
	
	// Update is called once per frame
	void Update () {
        HandleFriction();
	}

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");


        storeDir = cam.right;

        if (onGround)
        {

            rigidBody.AddForce(((storeDir * horizontal) + (cam.forward * vertical)) * speed / Time.deltaTime);

            if (jumpInput)
            {
                anim.SetTrigger("Jump");
                rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }

        directionPos = transform.position + (storeDir * horizontal) + (cam.forward * vertical);

        Vector3 dir = directionPos - transform.position;

        dir.y = 0;

        float animValue = Mathf.Abs(vertical) + Mathf.Abs(horizontal);


        anim.SetFloat("Forward", animValue, .1f, Time.deltaTime);


        if (horizontal != 0 || vertical != 0)
        {

            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir));

            if (angle != 0)
            {
                rigidBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed);
            }
        }

    }


   void OnCollisionEnter(Collision other)
    {
        onGround = true;
        rigidBody.drag = 5;
        anim.SetBool("onAir", false);
        anim.SetTrigger("HitGround");
    }

    void OnCollisionExit(Collision other)
    {
        onGround = false;
        rigidBody.drag = 5;
        anim.SetBool("onAir", true);
    }
    void HandleFriction()
    {
        // if there was no input
        if (horizontal == 0 && vertical == 0)
        {
            // we are stationary so max friction.
            capCol.material = mfriction;
        }
        else
        {
            capCol.material = zfriction;

        }
    }

    void SetupAnimator()
    {
        anim = GetComponent<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }
}

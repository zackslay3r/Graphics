using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerV2 : MonoBehaviour {

    public Transform target;
    public float lookSmooth = 0.09f;
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
    public float xTilt = 10;

    Vector3 destination = Vector3.zero;
    ChararacterControll charController;
    float rotateVel = 0;


	// Use this for initialization
	void Start () {
        SetCameraTarget(target);

	}

    void SetCameraTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<ChararacterControll>())
            {
                charController = target.GetComponent<ChararacterControll>();
            }
            else
                Debug.LogError("No Character Controller.");
        }
        else
        {
            Debug.LogError("No target.");
        }
    }

	// Update is called once per frame
	void LateUpdate () {
        MoveToTarget();

        LookAtTarget();
		
	}

    void MoveToTarget()
    {
        destination = charController.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);

    }

}

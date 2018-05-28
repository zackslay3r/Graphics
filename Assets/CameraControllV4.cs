using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllV4 : MonoBehaviour {

    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public Transform follow;
    public Vector3 targetPosition;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        transform.LookAt(follow);

	}
}

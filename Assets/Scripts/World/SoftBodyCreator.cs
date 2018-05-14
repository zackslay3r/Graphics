using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBodyCreator : MonoBehaviour
{
    public int Length, Height, Width;
    public float springCoefficient, dampering, scale, breakforce, mass;
    public GameObject softbody;

    public float offset;
    public GameObject[,,] arrayOfCircles;

    // At the start of the game, make a softbody.
    void Start()
    {
        MakeSoftBody(Length, Height, Width, springCoefficient, dampering);
    }


    public void MakeSoftBody(int length, int height,int width, float springCoefficient, float dampening)
    {
        // THis is a 3d Array.
        arrayOfCircles = new GameObject[length,height,width];





        // Creation of the spheres.
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {

                    GameObject g = Instantiate(softbody);
                    Rigidbody softBodyCircle = g.GetComponent<Rigidbody>();
                    softBodyCircle.mass = mass;
                    g.transform.localScale = new Vector3(scale,scale,scale);
                 
                    g.transform.position = transform.position;
                    arrayOfCircles[i, j, k] = g;
             
                    g.transform.position = new Vector3(transform.position.x + i * offset, transform.position.y + j * offset, transform.position.z + k * offset);
                    g.gameObject.tag = "Softbody";
                   
                }

               
            }
        
        }


        bool stopPls = true;
        // Creation of springs.
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    Collider[] results = Physics.OverlapSphere(arrayOfCircles[i, j, k].gameObject.transform.position, offset * 1.414213f);

                    foreach (Collider c in results)
                    {
                        if (stopPls == false)
                        {
                            stopPls = true;
                            continue;
                        }
                        if (c.gameObject.tag == "Softbody")
                        {

                            
                         
                            if (j == length)
                            {
                                continue;
                            }

                           
                            // collision objects
                            SpringJoint sj = c.gameObject.AddComponent<SpringJoint>() as SpringJoint;
                            sj.tolerance = 0;
                            sj.spring = springCoefficient;
                            sj.damper = dampening;
                            sj.breakForce = breakforce;
                         
                            Rigidbody rb = c.GetComponent<Rigidbody>();
                            Rigidbody ourRb = arrayOfCircles[i, j, k].GetComponent<Rigidbody>();

                                    if (rb != null && ourRb != null && rb != ourRb)
                                    {
                                        sj.connectedBody = ourRb;
                                    }

                        }

                    }

                }
            }

        }


        // Destorying all the excess joints that were created initally.
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    SpringJoint[] springJointsEachItem = arrayOfCircles[i, j, k].GetComponents<SpringJoint>();

                    foreach (SpringJoint joint in springJointsEachItem)
                    {
                        if (joint.connectedBody == null)
                        {
                            Destroy(joint);
                        }
                    }
                }
            }
        }
    }
}
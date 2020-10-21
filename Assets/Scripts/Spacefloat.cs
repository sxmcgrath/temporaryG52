using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Spacefloat : MonoBehaviour {
 
     public Transform waypointA, waypointB;
     private Transform target;
     public float speed = 10;
     public float spin = 10;
     void Update ()
     {
         transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
         transform.Rotate(spin,spin,spin, Space.Self);
         // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.005f)
        {
            spin*=-1;
            if(target == waypointA){
                target = waypointB;
            }
            else{
                target = waypointA;
            }
        }
     }

     void Start(){
         transform.position = waypointA.position;
         target = waypointB;
     }


 }

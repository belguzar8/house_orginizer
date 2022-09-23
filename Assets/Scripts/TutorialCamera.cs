using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EKTemplate;


public class TutorialCamera : MonoBehaviour
{
  /*  public Transform target;
    public Vector3 offset;
    public float camSpeed = 10;
    public float additionalPos;
    public Tutorial tutorial;
    public bool tutorialEnd = false;

    void FixedUpdate()
    {
        Vector3 pos = new Vector3(target.position.x, target.position.y, target.position.z) +
        (Vector3.left * offset.x) + (Vector3.forward * offset.z) + (target.up * offset.y) + new Vector3(0, 0, 0);
        transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * camSpeed);

        Vector3 dir = new Vector3(target.position.x, target.position.y, target.position.z + additionalPos) + new Vector3(0, 0, 0f) - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.fixedDeltaTime * camSpeed);
    }*/

     public Transform target;    
     public float speed;
     public Vector3 distance;
     public float rotationSpeed;
     public Tutorial tutorial;
     public bool tutorialEnd=false;

     private void Start()
     {
         Debug.Log("tutorialcam çalışıyo");
     }

     private void LateUpdate()
     {
         if (tutorialEnd && Vector3.Distance(target.position,transform.position)<.1f && Quaternion.Angle(transform.rotation,target.rotation)<.1f)
         { 
             
            // tutorial.tutorialIndex++;         
            // this.enabled = false;
         }
         transform.position = Vector3.Lerp(transform.position, target.transform.position, speed*Time.deltaTime);
         transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);
     }
}

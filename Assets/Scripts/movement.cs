using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class movement : MonoBehaviour
{
    private bool isRunning = false;
    public Joystick joystick;
    public float speed = 5f;
    private Rigidbody rb;
    private Animator anim;
    private Transform camTransform;

    private bool checkDoor = false;

    public RollOfTheFloor rolScript;
    public FollowRoll followRollScript;
    public bool isOpened;
    public bool movementPermission = true;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        camTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        #region Movement

        if (movementPermission)
        {
            rb.isKinematic = false;

            if (joystick.Direction == Vector2.zero)
            {
                rb.velocity = Vector3.zero;
                if (isRunning)
                {
                    isRunning = false;
                    anim.SetBool("Idle", true);
                    anim.SetBool("Run", false);
                }
                return;
            }




            if (!isRunning)
            {
                isRunning = true;
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);

            }

            Vector3 camRight = camTransform.right;
            Vector3 camForward = camTransform.forward;
            camForward.y = 0f;
            Vector3 dir = camRight * joystick.Horizontal + camForward * joystick.Vertical;
            transform.forward = dir;
            rb.velocity = dir * speed;
        }

        else
        {
            rb.isKinematic = true;
            anim.SetBool("Run", false);

        }


        #endregion
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("door"))
        {
            if (checkDoor) return;

            other.transform.GetChild(0).DORotate(new Vector3(-90, 0, -135), 0.4f);

            checkDoor = true;
        }
        else if (other.CompareTag("house"))
        {
            MakeHouseTransparent(other.transform.parent.GetComponent<MeshRenderer>());
        }
    }

    private void MakeHouseTransparent(MeshRenderer mr)
    {
       // Debug.Log("MakeHouseTransparent");
       // Debug.Log(mr.name);
        Material[] temp = mr.materials;
        for (int i = 0; i < temp.Length; i++)
        {
            Color tempColor = temp[i].color;
            tempColor.a = 0;
            temp[i].color = tempColor;
        }
        mr.materials = temp;
    }

    private void MakeHouseOpaque(MeshRenderer mr)
    {
       // Debug.Log("MakeHouseOpaque");
        Material[] temp = mr.materials;
        for (int i = 0; i < temp.Length; i++)
        {
            Color tempColor = temp[i].color;
            tempColor.a = 1;
            temp[i].color = tempColor;
        }
        mr.materials = temp;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("door"))
        {
            other.transform.GetChild(0).DORotate(new Vector3(-90, 0, 0), 0.4f);

            checkDoor = false;
        }
        else if (other.CompareTag("house"))
        {
            MakeHouseOpaque(other.transform.parent.GetComponent<MeshRenderer>());
        }
    }
}

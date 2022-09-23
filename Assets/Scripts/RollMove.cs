using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMove : MonoBehaviour
{
    
    public float speed = 5;
    
    void Start()
    {
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    
}

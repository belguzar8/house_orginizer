using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    public Tutorial tutorialScript;
    private Transform mainCamera;
    private Transform arrowObj;
    
   

    void Start()
    {
        mainCamera = Camera.main.transform;
        arrowObj = transform.GetChild(0).transform;
        transform.DOMoveY(transform.position.y - 3, 0.5f).SetLoops(-1, LoopType.Yoyo);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("character"))
        {
            tutorialScript.ClosedTutorial();
        }
    }

    private void Update()
    {
        arrowObj.rotation = Quaternion.LookRotation(mainCamera.position - arrowObj.position, mainCamera.up);
    }

}

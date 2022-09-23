using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Transform cameraTransform;
    public Text counterText;
    public Image counterImage;

    public void SetCounterValues(int frame, float fillAmount)
    {
        //Debug.Log( " CounterText " + frame/100f+ " fillAmount "+fillAmount);
        counterText.text = (frame / 100f).ToString();
        counterImage.fillAmount = fillAmount;
    }

    void Start()
    {
        counterText.enabled = false;
        counterImage.enabled = false;

    }
   
    void Update()
    {
        transform.rotation = cameraTransform.rotation; 
    }
}

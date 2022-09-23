using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayingOrder : MonoBehaviour
{
    
    public bool color1Completed;
    public bool color2Completed;
    public bool color3Completed;

    bool step1 = false;
    bool step2 = false;
    bool step3 = false;


    public void ScanControlPermission()
    {
        if(color3Completed && !color1Completed && !color2Completed)
        {
            step1 = true;
        }

        if (color3Completed && color2Completed && !color1Completed)
        {
            step2 = true;
        }

        if (color3Completed && color2Completed && color1Completed)
        {
            step3 = true;
        }


        if(step1 & step2 & step3)
        {
            Debug.Log("Success");
        }

        else
        {
            Debug.Log("Fail");
        }
    }

}

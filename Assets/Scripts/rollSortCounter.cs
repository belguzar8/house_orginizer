using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollSortCounter : MonoBehaviour
{
    public bool[] SelfRolls;
    public GameObject[] selfRollObjects;
    public GameObject otherColorRoll;
    public LayingOrder laying;
    public bool layingColor1, layingColor2, layingColor3;

    public void isSuccess()
    {
        for(int i=0; i<SelfRolls.Length; i++)
        {
            if (!SelfRolls[i])
            {
                if (layingColor1)
                    laying.color1Completed = false;
                else if (layingColor2)
                    laying.color2Completed = false;
                else if (layingColor3)
                    laying.color3Completed = false;

                break;
            }

            else
            {
                if(i == SelfRolls.Length -1)
                {
                    if (layingColor1)
                        laying.color1Completed = true;
                    else if (layingColor2)
                        laying.color2Completed = true;
                    else if (layingColor3)
                        laying.color3Completed = true;

                    break;
                }
            }
        }

        laying.ScanControlPermission();
        
    }
}

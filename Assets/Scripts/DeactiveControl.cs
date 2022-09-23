using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveControl : MonoBehaviour
{
    bool isRedLayerCompleted;

    private void Start()
    {

        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("This Gameobject Dead Now") == 1)
        {
            this.gameObject.SetActive(false);
            
        }

    }

    public void Control()
    {
        for(int i=0; i <= transform.childCount-1; i++)
        {
            if(transform.GetChild(i).name != "Dead")
            {
                break;
            }

            else
            {
                if(i<= transform.childCount -1)
                {
                    //this.gameObject.SetActive(false);
                    PlayerPrefs.SetInt("This Gameobject Dead Now", 1);
                    break;
                }
            }
        }
    }
}

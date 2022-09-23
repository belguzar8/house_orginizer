using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Affordable : MonoBehaviour
{
    public enum AffordableType { Box, Home }  //satın alınacak makine ve ev
    public GameObject OpenModels; //açılacak model
    public GameObject BackGround; 
    public TextMeshPro CostTMP; //maliyet texti
    public AffordableType Type;
    public bool isBought; //satın alındı
    public int cost;  //maliyet
    public GameObject transparent;
    public Tutorial tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("character"))
        {
            BuyArea();            
        }        
    }

    public void BuyArea()   //SATIN ALINAN ALAN
    {
        if (!isBought)   
        {
            isBought = true;  //SATIN ALINDI
        }
        if (Type == AffordableType.Box )  //MAKİNE TİPİ SATIN ALINDI
        {
            GetComponent<BoxCollider>().enabled = false;           
            OpenModels.SetActive(true);           
            CostTMP.gameObject.SetActive(false);
            BackGround.SetActive(false);

            if (tutorial.tutorialIndex == 1)
            {
                tutorial.TutorialPoint();
            }
                           
        }
        else if (Type==AffordableType.Home)
        {
            GetComponent<BoxCollider>().enabled = false;
            OpenModels.SetActive(true);
            CostTMP.gameObject.SetActive(false);
            BackGround.SetActive(false);
            transparent.SetActive(true);
            if (tutorial.tutorialIndex==2)
            {
                tutorial.TutorialPoint();
            }

        }
    }

    
}

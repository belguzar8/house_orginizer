using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EKTemplate;

public class Tutorial : MonoBehaviour
{
    public GameObject[] arrows;
    public Transform[] cameraPoints;
    public Text tutorialText;
    public int tutorialIndex;
    public GameObject mainCamera;
    public TutorialCamera tc;
    bool isSecond;
    bool isThird;
    bool isFourth;
    
    public movement charMov;

    void Start()
    {        
        TutorialPoint();        
    }

    private IEnumerator ActivateSecondTutorial()
    {
        if (!isSecond)
        {
            arrows[0].SetActive(true);
            arrows[1].SetActive(true);
            arrows[2].SetActive(true);

            arrows[3].SetActive(true);
            tc.enabled = true;
            tc.tutorialEnd = false;
            tc.target = cameraPoints[1];
            tutorialText.text = "Buy Home";
            yield return new WaitForSeconds(2f);
            tc.target = cameraPoints[0];
            tc.GetComponent<CameraManager>().enabled = true;
            tc.GetComponent<TutorialCamera>().enabled = false;
            isSecond = true;
        }     
    }

    private IEnumerator ActivateThirdTutorial()
    {
        if (!isThird&&isSecond)
        {
            isThird = true;
            arrows[4].SetActive(true);
            charMov.movementPermission = false;  //karakter hareket izni yok
            charMov.GetComponent<Animator>().SetBool("Run", false);  //karakter koşma animasyonu aktif değil
            charMov.GetComponent<Animator>().SetBool("Idle", true);  //karakter duruş animasyonu aktif
            tc.enabled = true;
            tc.tutorialEnd = false;
            tc.target = cameraPoints[0];
            tutorialText.text = "Take a 7 roll";          
            tc.GetComponent<CameraManager>().enabled = false;
            tc.GetComponent<TutorialCamera>().enabled = true;
            yield return new WaitForSeconds(2f);
            tc.target = cameraPoints[1];
            yield return new WaitForSeconds(0.3f);
            tc.GetComponent<CameraManager>().enabled = true;
            tc.GetComponent<TutorialCamera>().enabled = false;
            charMov.movementPermission = true;  //karakter hareket izni var
        }                      
    }

    public IEnumerator ActivateFourthTutorial()
    {
        if (!isFourth && isThird)
        {
            isFourth = true;
            arrows[5].SetActive(true);
            arrows[6].SetActive(true);
            tc.enabled = true;
            tc.tutorialEnd = false;
            tc.target = cameraPoints[3];
            tutorialText.text = "Put 3 rolls";
           
            tc.GetComponent<CameraManager>().enabled = false;
            tc.GetComponent<TutorialCamera>().enabled = true;

            yield return new WaitForSeconds(4f);
            //arrows[5].SetActive(false);
           // arrows[6].SetActive(false);           
            arrows[7].SetActive(true);
            tc.enabled = true;
            tc.tutorialEnd = false;
            tc.target = cameraPoints[3];
            Debug.Log("put1roll");
            tutorialText.text = "Put 1 roll";
            tc.GetComponent<TutorialCamera>().enabled = true;
            tc.GetComponent<CameraManager>().enabled = false;

            yield return new WaitForSeconds(4f);

            arrows[9].SetActive(true);
            arrows[10].SetActive(true);
            arrows[11].SetActive(true);
            tutorialText.text = "Collect The Rolls";                     
            tc.GetComponent<CameraManager>().enabled = false;
            tc.GetComponent<TutorialCamera>().enabled = true;

            yield return new WaitForSeconds(4f);
            
            arrows[12].SetActive(true);
            arrows[13].SetActive(true);
            arrows[14].SetActive(true);
            tc.enabled = true;
            tc.tutorialEnd = false;
            tc.target = cameraPoints[1];           
            tutorialText.text = "Put The Roll On The Floor";
            yield return new WaitForSeconds(4f);
            tc.enabled = true;
            tc.tutorialEnd = true;
            tc.target = cameraPoints[3];
            tc.GetComponent<TutorialCamera>().enabled = false;
            tc.GetComponent<CameraManager>().enabled = true;
            


        }
    }
     
    public void TutorialPoint()
    {
        if (tutorialIndex==0) 
        {
            arrows[0].SetActive(true);
            arrows[1].SetActive(true);
            arrows[2].SetActive(true);
            tc.enabled = true;
            tc.tutorialEnd = false;
            tc.target = cameraPoints[0];
            tutorialText.text = "Buy Machines";
            //tc.GetComponent<TutorialCamera>().enabled = true;
            //tc.GetComponent<CameraManager>().enabled = false;
        }
        else if (tutorialIndex==1)
        {
            StartCoroutine(ActivateSecondTutorial());
        }
        else
        {
            if (!isThird)
            {
                StartCoroutine(ActivateThirdTutorial());
            }
       else   
            if (!isFourth)
            {
                StartCoroutine(ActivateFourthTutorial());
            }     
        }
        tutorialIndex++;
        tutorialText.gameObject.SetActive(true);
        tutorialText.transform.DOScale(Vector3.one * 1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void ClosedTutorial()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(false);
        }
        tutorialText.gameObject.SetActive(false);
    }
}

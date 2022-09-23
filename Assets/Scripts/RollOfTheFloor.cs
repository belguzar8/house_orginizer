using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollOfTheFloor : MonoBehaviour  //RULONUN ZEMİNE SERİLMESİ
{
    public float speed;         //rulonun açılma hızı
    public Transform cube;      //rulonun altından uzanacak olan küp
    public Vector3 sizeChange;  //uzayacak olan küpün scaleinin belirlenmesi
    public Transform endPoint;  //rulonun uzanacağı nokta
    public Animator animator;
    public movement movementEvent;
    public Transform ghost;
    public string rollKey;
    

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt(rollKey,0)==1)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            cube.GetComponent<BoxCollider>().enabled = true;
            ActivateRoll();
        }
        else
        {
            GetComponent<CapsuleCollider>().enabled = true;
            cube.GetComponent<BoxCollider>().enabled = false;
          
        }


        if (PlayerPrefs.GetInt("" + rollKey + "IsOpened") != 1)
        {
            //this.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("character"))
        {

            PlayerPrefs.SetInt("" + rollKey + "IsOpened", 1);


            movementEvent.movementPermission = false;
            other.gameObject.transform.DOMove(ghost.position, 1f);
            other.gameObject.transform.DORotate(ghost.eulerAngles, 1f);
            animator.SetTrigger("throwing");
            other.GetComponent<movement>().rolScript = GetComponent<RollOfTheFloor>();
            ActivateRoll(other.transform);
            
        }
    }

    public void ActivateRoll(Transform other=null)
     {
        if (other != null) StartCoroutine(Delay(other.transform));
        else StartCoroutine(Delay());

        IEnumerator Delay(Transform other=null)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            if (other!=null) yield return new WaitForSeconds(1.5f);
            
            float distance = Vector3.Distance(transform.position, endPoint.position);
            float tweenDuration = distance * 0.1f;
            if (other == null) tweenDuration = 0f; 
            cube.DOMove(endPoint.position, tweenDuration).SetEase(Ease.OutQuad);   //küpü endpoint noktasına taşıyoruz
            cube.DOScaleX(distance*0.515f, tweenDuration).SetEase(Ease.OutQuad); //distance*0.515f küpü taşırken x ekseninde scale edip uzamasını sağladık
            transform.DOMove(endPoint.position, tweenDuration).OnComplete(() => movementEvent.isOpened=false).SetEase(Ease.OutQuad); //ruloyu endpointe taşıyoruz
            transform.DOScaleX(0f, tweenDuration).SetEase(Ease.OutQuad);      //rulonun xini scale edip küçültüyoruz
            transform.DOScaleZ(0f, tweenDuration).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    movementEvent.movementPermission = true;
                    cube.GetComponent<BoxCollider>().enabled = true;
                    PlayerPrefs.SetInt(rollKey,1);

                }
                );    //rulonun zsini scale edip küçültüyoruz
            yield return new WaitForSeconds(2f);

            if (other != null) other.transform.DORotate(new Vector3(0, other.transform.eulerAngles.y, 0), 1f);

        }
     }

  



}

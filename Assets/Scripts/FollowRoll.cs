using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowRoll : MonoBehaviour  //küp scripti
{
    public Transform roll;  //takip edilen rulo
    public Transform startPoint; //rulonun katlanacağı yöndeki nokta
    public Animator animator;
    public movement movementEvent;
    public Transform ghost;

    private void Update()
    {
        transform.position = roll.transform.position;  //küpün transformu rulonun transformuna eşitlendi
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("character"))
        {
            movementEvent.movementPermission = false;
            other.gameObject.transform.DOMove(ghost.position, 1f);
            other.gameObject.transform.DORotate(ghost.eulerAngles, 1f);
            animator.SetTrigger("throwing");
            other.GetComponent<movement>().followRollScript = GetComponent<FollowRoll>();
            notActivateRoll();            
        }


        if (other.CompareTag("SortCounter"))
        {
            
            var control = other.GetComponent<rollSortCounter>();

            if (other.gameObject.layer == this.gameObject.layer && control.SelfRolls[0] == false)
            {
                control.SelfRolls[0] = true;
            }
            else if (other.gameObject.layer == this.gameObject.layer && control.SelfRolls[1] == false)
            {
                control.SelfRolls[1] = true;
            }
            else if (other.gameObject.layer == this.gameObject.layer  && control.SelfRolls[2] == false)
            {
                control.SelfRolls[2] = true;
            }

            control.isSuccess();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SortCounter"))
        {
            var control = other.GetComponent<rollSortCounter>();

            if (other.gameObject.layer == this.gameObject.layer && control.SelfRolls.Length == 1 && control.SelfRolls[0] == true)
            {
                control.SelfRolls[0] = false;
            }
            else if (other.gameObject.layer == this.gameObject.layer && control.SelfRolls.Length == 2 && control.SelfRolls[1] == true)
            {
                control.SelfRolls[1] = false;
            }
            else if (other.gameObject.layer == this.gameObject.layer && control.SelfRolls.Length == 3 && control.SelfRolls[2] == true)
            {
                control.SelfRolls[2] = false;
            }
        }
    }

    public void notActivateRoll()
  {
        StartCoroutine(Delay());
        IEnumerator Delay()

        {
            GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(2f);
            float distance = Vector3.Distance(transform.position, startPoint.position);
            float tweenDuration = distance * 0.1f;
            transform.DOMove(startPoint.position, tweenDuration).OnComplete(() => movementEvent.isOpened = true).SetEase(Ease.OutQuad); //küpü startpoint noktasına taşıyoruz
            transform.DOScaleX(0f, tweenDuration).SetEase(Ease.OutQuad); //küpü taşırken x ekseninde scalei küçültüyoruz
            Debug.Log("tweenDuration= " + tweenDuration );
            roll.DOMove(startPoint.position, tweenDuration).SetEase(Ease.OutQuad); //ruloyu startPointe taşıyoruz
            roll.DOScaleX(1f, tweenDuration).SetEase(Ease.OutQuad); //rulonun xini scale edip büyütüyoruz
            roll.DOScaleZ(1f, tweenDuration).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                movementEvent.movementPermission = true;
                roll.GetComponent<CapsuleCollider>().enabled = true;
                PlayerPrefs.SetInt(roll.GetComponent<RollOfTheFloor>().rollKey, 0);
            }
            ); //rulonun zsini scale edip büyütüyoruz }

        }
    }
}


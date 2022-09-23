using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Machine : MonoBehaviour
{

   
    public List<GameObject> rawRollList = new List<GameObject>();
    public Transform firstPoint;  //kağıtların masaya bırakılacağı ilk nokta
    public GameObject firstPrefab;
    public GameObject secondPrefab;
    public Transform secondPoint;
    public Transform midPoint;
    public List<GameObject> productRollList = new List<GameObject>();
    bool isStartProduced;


    public RuloType stackableRulos; //stacklenebilecek rulolar
    public List<Rulos> stackedRulos { private set; get; } //stacklenen ruloları verir


    private void Start()
    {
        DOTween.Init();
    }

    private void Update()
    {
        WorkStart();
    }

    public void SetMachinePoint(RuloType ruloTypes) //bu fonksiyon machinepointin tanımı, tanım gereksinimi de rulotype listesi
    {
        this.stackableRulos = ruloTypes;

    }
    public bool CanBeAdded(RuloType ruloType)  //rulo tipi bu makinaya eklenebilir mi?
    {
        return stackableRulos == ruloType;
    }

  




    IEnumerator MachineProduced() //renkli rulolar çıktısı
    {
        isStartProduced = true;

        int rawRollListCount = rawRollList.Count;
        if (rawRollListCount > 0)
        {
            yield return new WaitForSeconds(0.5f); //0.5 idi
            List<Vector3> rulosPos = new List<Vector3>();
            for (int i = 0; i < rawRollList.Count; i++)
            {
                rulosPos.Add(rawRollList[i].transform.position);
            }
            for (int i = 1; i < rawRollList.Count; i++)
            {
                
                rawRollList[i].transform.DOMove(rulosPos[i - 1], 0.5f);
            }
            Transform rawRoll = rawRollList[0].transform;
            rawRoll.DOMove(midPoint.position, 0.5f);

            yield return new WaitForSeconds(0.5f); //0.5 idi
            RemoveLast();
            GameObject product = Instantiate(secondPrefab);  //2.noktada renkli ruloyu instantiate et
            product.transform.position = midPoint.position; //0 yerine 1.6f
            product.transform.DOMove(secondPoint.position,0.5f); //*2F İDİ
            if (productRollList.Count > 0)
            {
                Vector3 firstRuloPos = secondPoint.position;
                for (int i = 0; i < productRollList.Count; i++)
                {
                    productRollList[i].transform.DOMove(firstRuloPos + (productRollList.Count-i) * Vector3.up * 4.3f, 0.5f);
                }
            }

            productRollList.Add(product);

            yield return new WaitForSeconds(0.5f); //0.5 idi


        }
        isStartProduced = false;

    }

    public void WorkStart()
    {
        if (!isStartProduced)
        {
            StartCoroutine(MachineProduced());
        }

    }

    public void RemoveLast() //sondakini kaldır
    {
        if (rawRollList.Count > 0)
        {
            Destroy(rawRollList[rawRollList.Count - 1]); //8 5 9, 3 elemanlı, ama saymaya 0 1 2 diye başlarız, boyut-1=2
            rawRollList.RemoveAt(rawRollList.Count - 1);
        }
    }



}

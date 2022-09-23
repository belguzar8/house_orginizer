using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBase : MonoBehaviour //RULOLARIN ÇOĞALTILMASI 
{
    public bool produce;   
    public List<GameObject> rawRollList= new List<GameObject>(); //oluşturduğumuz rulo objelerini bir listede tuttuk
    public GameObject rollPrefab; //kutuda üretilecek olan rulo objesi
    public Transform exitPoint; //oluşturulan rulonun nerede dizildiğini belli eder
    bool isWorking; //rulonun üretim yerinin(yazıcı) çalışıp çalışmadığını kontrol eder

    int stackCount = 5;

    void Start()
    {
        if(produce) StartCoroutine(Roll());
    }

    public void RemoveLast() //oluşan ruloları almaya geldiğinde sondakini kaldır diycez
    {
        if (rawRollList.Count > 0)
        {
            Destroy(rawRollList[rawRollList.Count - 1]); //8 5 9, 3 elemanlı, ama saymaya 0 1 2 diye başlarız, boyut-1=2
            rawRollList.RemoveAt(rawRollList.Count - 1);
        }

    }

    IEnumerator Roll()  //her bir saniyede rulo üretmesini istiyoruz,bunu Updatte kontrol edemeyiz,rulo üretimi(yazıcı),void startta çağırdık
    {
        while (true)  //sürekli çalışsın istediğimiz için dedik
        {
            float rollCount = rawRollList.Count;
            int rowCount = (int)rollCount / stackCount; //rowcount sütun sayısı,stackcount sütunların kaç rulodan oluşacağı-10arlı-20li
            if (isWorking == true) //eğer çalışıyorsa
            {
                GameObject temp = Instantiate(rollPrefab);  //ruloyu çoğalttık
                temp.GetComponent<Rulos>().SetRuloType(RuloType.White);
                //oluşturduğumuz nesnesin pozisyonunu nerede oluşmasını istiyorsak ona eşitledik
                //y'si, listede kaç eleman varsa o olacak                                                             //20 idi //2idi
                temp.transform.position = new Vector3(exitPoint.position.x, 2f+(rollCount%stackCount) * 4.3f, exitPoint.position.z); //rowCount*5 ile yan yana dizilmesini sağladık,yan yana olsun istenmezse *5i sil
               
                rawRollList.Add(temp); //oluşturduğumuz tempi rulo listesine ekle

                if (rawRollList.Count >= 30) //30 taneden fazla üretmesin, değiştirebilirsin
                {
                    isWorking = false;
                }
            }
            
            else if (rawRollList.Count < 30) //önünde 30 taneden az varsa çalışsın
            {
                isWorking = true;
            }
            yield return new WaitForSeconds(0.5f); //1 saniyede bir rulo üretilir
        }
       
    }
}

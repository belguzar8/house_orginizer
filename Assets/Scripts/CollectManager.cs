using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class CollectManager : MonoBehaviour
{
    [SerializeField] private Transform Character;
    public Transform collectPoint; //karakterin ruloları taşıyacağı nokta
    public List<GameObject> rollList = new List<GameObject>();  //toplanan rulo listesi
    public GameObject rollPrefab;
    int rollLimit = 30; //eline en fazla 10 rulo alabilir
    public List<GameObject> newRolls = new List<GameObject>();
    int stackCount = 10;
    public movement movementEvent;
    public RollBase rollBase;
    public Counter counter;

    bool isGetBaseStay;
    bool isGetMachineStay;
    bool isGiveMachineStay;
    bool isGiveBreakStay;
    bool isGiveTrashStay;

    float getBaseTime;
    float givingMachineTime;
    float getMachineTime;
    float givingBreakTime;
    float givingTrashTime;

    Machine lastStayMachine;
    BreakPoint lastStayBreakPoint;
    Transform lastTrash;

    [SerializeField] List<GameObject> redRollList;
    [SerializeField] List<GameObject> yellowRollList;
    [SerializeField] List<GameObject> orangeRollList;

    public Tutorial tutorial;
   

    void Start()
    {
        DOTween.Init();
        transform.DOLocalMove(Vector3.zero, 0.5f);
    }

    private void Update()
    {
        if (Time.time - 0.7f >= getBaseTime && isGetBaseStay)   //0.5f
        {
            getBaseTime = Time.time;
            GetRollFromBase();
        }
        else if (Time.time - 0.7f >= getMachineTime && isGetMachineStay && rollList.Count < rollLimit)
        {
            getMachineTime = Time.time;
            GetRollFromMachine(lastStayMachine);
        }
        else if (Time.time - 0.7f >= givingMachineTime && isGiveMachineStay)
        {
            givingMachineTime = Time.time;
            GiveRollFromMachine(lastStayMachine);
        }
        else if (Time.time - 7f >= givingBreakTime && isGiveBreakStay)  
        {
           // Debug.Log("givingbreaktime=time.time");
            givingBreakTime = Time.time;
            GiveToBreakPoint(lastStayBreakPoint);
        } 
        else if (Time.time - 0.7f>= givingTrashTime && isGiveTrashStay)
        {
            givingTrashTime = Time.time;
            TrashOut(lastTrash);
        }
    }
    void GetRollFromBase() //ruloyu topla
    {
        if (rollList.Count <= rollLimit && rollBase.rawRollList.Count > 0) //10 dan az rulo varsa elinde toplayabilir
        {
            float rollCount = rollList.Count;
            GameObject temp = rollBase.rawRollList[rollBase.rawRollList.Count - 1];
            temp.transform.SetParent(collectPoint);
            temp.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 0.6f);
            rollBase.rawRollList.RemoveAt(rollBase.rawRollList.Count - 1);
            float yPos = (rollList.Count % 5) * 46.5f;  //%8 5 yazdım 5li toplaması için              
            temp.transform.DOLocalMove(new Vector3(0, yPos, -(rollCount / 5) * 48f), 0.6f);  // /8, 5 yazdım 5li toplaması için           
            rollList.Add(temp);
        }
        if(rollList.Count>=6 && tutorial.tutorialIndex == 3)
        {
            Debug.Log("fourthtutorial");
            // tutorial.ActivateFourthTutorial();
            tutorial.TutorialPoint();
        }
            
       
    }
    public void GiveToBreakPoint(BreakPoint breakPoint)
    {
        GameObject obj = rollList.Find(x => x.GetComponent<Rulos>().myRuloType == breakPoint.stackableRulos);
        if (obj != null && breakPoint.CanBeAdded(obj.GetComponent<Rulos>().myRuloType))
        {
            breakPoint.AddStackedRulos(obj.GetComponent<Rulos>());            
            breakPoint.GetComponent<Collider>().enabled = false;
            isGiveBreakStay = false;
            Transform roll = obj.transform;
            roll.GetComponent<Collider>().enabled = false;
            roll.SetParent(breakPoint.transform);
            roll.DORotate(breakPoint.transform.eulerAngles, 0.5f); //0.6fs              
            roll.DOMove(breakPoint.transform.position + Vector3.up * (breakPoint.stackedRulos.Count - 1) * roll.localScale.y * 4.3f, 0.5f) //0.6f                                                                 
            .OnComplete(() =>
            {
                if(breakPoint.stackableRulos == RuloType.Red)
                {
                    redRollList[breakPoint.breakPointIndex].SetActive(true);                    
                }
                else if (breakPoint.stackableRulos == RuloType.Yellow)
                {
                    yellowRollList[breakPoint.breakPointIndex].SetActive(true);
                }
                else if (breakPoint.stackableRulos == RuloType.Orange)
                {
                    orangeRollList[breakPoint.breakPointIndex].SetActive(true);
                }
                obj.SetActive(false);
                givingBreakTime = Time.time;                
            });
            rollList.Remove(obj);
        }
    }
     public void GiveRollFromMachine(Machine machine) //ruloyu makinaya bırakırken 
  {
      if (rollList.Count > 0)
      {
            GameObject obj = rollList.Find(x=>x.GetComponent<Rulos>().myRuloType==machine.stackableRulos);
            if (obj!=null&&machine.CanBeAdded(obj.GetComponent<Rulos>().myRuloType))
            {
                if (machine.stackableRulos == RuloType.White)
                {
                    
                    Transform _roll = rollList[rollList.Count - 1].transform;
                    _roll.SetParent(machine.firstPoint);
                    _roll.DORotate(new Vector3(0f, 0f, 90f), 0.5f); //0.6f
                    _roll.DOMove(machine.firstPoint.position + Vector3.up * (machine.rawRollList.Count - 1) * _roll.localScale.y * 4.3f, 0.5f); //0.6f
                    RemoveLast();
                    machine.rawRollList.Add(_roll.gameObject);
                }
            }
           
        }
         
  }







    /* public void GiveRollFromMachine(Machine machine) //ruloyu makinaya bırakırken 
     {

         if (rollList.Count > 0)
         {
             Transform _roll = rollList[rollList.Count - 1].transform;
             _roll.SetParent(machine.firstPoint);
             _roll.DORotate(new Vector3(0f, 0f, 90f), 0.5f); //0.6f
             _roll.DOMove(machine.firstPoint.position + Vector3.up * (machine.rawRollList.Count - 1) * _roll.localScale.y * 4.3f, 0.5f); //0.6f
             RemoveLast();
             machine.rawRollList.Add(_roll.gameObject);
         }
     }*/


    public void GetRollFromMachine(Machine machine) //ruloyu makinadan al
    {
        if (machine.productRollList.Count > 0)
        {
            Transform _roll = machine.productRollList[0].transform;
            _roll.transform.SetParent(collectPoint);
            _roll.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 0.6f);
            machine.productRollList.RemoveAt(0);

            float yPos = (rollList.Count % 5) * 46.5f;  //%8 5 yazdım 5li toplaması için              
            _roll.transform.DOLocalMove(new Vector3(0, yPos, -(rollList.Count / 5) * 48f), 0.6f);  // /8, 5 yazdım 5li toplaması için           
            rollList.Add(_roll.gameObject);

        }
    }
    public void RemoveLast() //topladığın ruloyu bırakırken sondakini kaldır
    {
        if (rollList.Count > 0)
        {
            rollList.RemoveAt(rollList.Count - 1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("trash"))
        {
            isGiveTrashStay = true;
            lastTrash = other.transform;
        }
        else if (other.gameObject.CompareTag("BreakPointArea"))
        {
            // Debug.Log("BreakPointArea");
            isGiveBreakStay = true;
            lastStayBreakPoint = other.GetComponent<BreakPoint>();
        }
        else if (other.gameObject.CompareTag("BaseCollectArea"))
        {
            isGetBaseStay = true;
        }
        else if (other.gameObject.CompareTag("MachineCollectArea"))
        {
            isGetMachineStay = true;
            lastStayMachine = other.transform.parent.parent.GetComponent<Machine>();
        }
        else if (other.gameObject.CompareTag("MachineGivingArea"))
        {
            isGiveMachineStay = true;
            lastStayMachine = other.transform.parent.parent.GetComponent<Machine>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("trash"))
        {
            isGiveTrashStay = false;
        }
        else if (other.gameObject.CompareTag("BreakPointArea"))
        {
            other.gameObject.name = "Dead";
            other.gameObject.transform.parent.GetComponent<DeactiveControl>().Control();

            isGetBaseStay = false;
        }
        else if (other.gameObject.CompareTag("BaseCollectArea"))
        {

            isGetBaseStay = false;

        }
        else if (other.gameObject.CompareTag("MachineCollectArea"))
        {

            isGetMachineStay = false;

        }
        else if (other.gameObject.CompareTag("MachineGivingArea"))
        {
            isGiveMachineStay = false;
        }
    }
    private void TrashOut(Transform trashPoint)
    {
        if (rollList.Count > 0)
        {
            Transform trashRoll = rollList[rollList.Count - 1].transform;
            rollList.RemoveAt(rollList.Count - 1);
            trashRoll.transform.SetParent(trashPoint);
            trashRoll.DOScale(Vector3.zero, 1f).SetEase(Ease.InQuad);
            trashRoll.DOLocalRotate(Vector3.zero, 1f);
            trashRoll.DOJump(trashPoint.position, 15f, 1, 1f)
            .OnComplete(() =>
            {
                Destroy(trashRoll.gameObject);
            }
            );
        }
    }

}

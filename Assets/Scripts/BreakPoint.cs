using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoint : MonoBehaviour
{

    public List<Rulos> stackedRulos { private set; get; } //stacklenen ruloları verir
    public RuloType stackableRulos; //stacklenebilecek rulolar
    public int breakPointIndex;
    private void Start()
    {
        stackedRulos = new List<Rulos>();
    }

    public void SetBreakPoint(RuloType ruloTypes) //bu fonksiyon breakpointin tanımı, tanım gereksinimi de rulotype listesi
    {
        this.stackableRulos = ruloTypes;
        stackedRulos = new List<Rulos>();
    }

    public bool CanBeAdded(RuloType ruloType)  //rulo tipi bu breakpointe eklenebilir mi?
    {
        return stackableRulos==ruloType;    
    }

    public void AddStackedRulos(Rulos addedRulos)  //ruloyu stackle
    {
        if (CanBeAdded(addedRulos.myRuloType))
        {
            stackedRulos.Add(addedRulos);    
        }
    }

   

}

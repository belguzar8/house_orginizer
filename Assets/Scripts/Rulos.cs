using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rulos : MonoBehaviour
{
    public RuloType myRuloType;
    
    public void SetRuloType(RuloType ruloType)
    {
        myRuloType = ruloType;
    }   
}
public enum RuloType
{
    White, Red, Blue, Green, Orange, Yellow,Purple
}

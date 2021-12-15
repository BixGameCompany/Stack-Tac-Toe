using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceManager : MonoBehaviour
{
    
    public int ownersIndex;
    public bool hasBeenPlaced;

    [System.Serializable]
    public enum pieceSize // your custom enumeration
 {
    Small, 
    Medium, 
    Large
 };
  public pieceSize Size;  // this public var should appear as a drop down
}

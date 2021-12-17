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
  public void setSize(pieceSize size) {

     Transform parent = transform.parent;
     transform.SetParent(null);

     Size = size;
     switch(Size){

        case pieceSize.Small:
            transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            transform.SetParent(parent);
        break;
        case pieceSize.Medium:
            transform.localScale = new Vector3(0.75f,0.75f,0.75f);
            transform.SetParent(parent);
        break;
        case pieceSize.Large:
            transform.localScale = new Vector3(1,1,1);
            transform.SetParent(parent);
        break;
     

     }





  }
}

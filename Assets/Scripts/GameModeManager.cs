using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            //RandomizePieceSize(GameObject.FindGameObjectsWithTag("GamePieces"));
        }
    }
    public void RandomizePieceSize(GameObject singlePiece){

    }
    public void RandomizePieceSize(GameObject[] multiplePieces){
        foreach(GameObject g in multiplePieces){
            if(g.GetComponent<pieceManager>().hasBeenPlaced){
                pieceManager.pieceSize[] test = {pieceManager.pieceSize.Small,pieceManager.pieceSize.Medium,pieceManager.pieceSize.Large};
                g.GetComponent<pieceManager>().setSize(test[Random.Range(0,3)]);
            }
            
        }
    }
}

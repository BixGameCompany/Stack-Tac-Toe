using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OfflinePlayerController : MonoBehaviour
{
    public bool isPaused{ get; set; }
    public OfflineGameManager OMG;
    public winCheck WC;
    public int PlayerIndex;
    Camera cam;
    RaycastHit hit;
    public bool canPlay;
    public GameObject selectedGamePiece;
    public bool holdingPiece;
    Vector3 lastHitPosition;
    Vector3 gamePieceStartPos;
    float distance;
    GameObject[] allGamePieces;

    


    void Start()
    {
        OMG = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OfflineGameManager>();
        WC = GameObject.FindGameObjectWithTag("GameManager").GetComponent<winCheck>();
        cam = GetComponentInChildren<Camera>();
        allGamePieces = GameObject.FindGameObjectsWithTag("GamePieces");
    }
    void Update()
    {
        if(!isPaused){
            
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        

        if(hit.transform != null){
            lastHitPosition = hit.transform.position;
        }
        //Selected the Piece that will be moved
        if(Physics.Raycast(ray,out hit) && Input.GetMouseButtonDown(0) && canPlay){

            if(selectedGamePiece == null && holdingPiece==false){

                if(hit.transform.gameObject.CompareTag("GamePieces")&& checkGamePiece(hit.transform.gameObject,PlayerIndex))
                {
                    selectedGamePiece = hit.transform.gameObject;
                    gamePieceStartPos = selectedGamePiece.transform.position;
                    holdingPiece = true;
                    
                    setGamePieceLayer(2);

                    Debug.Log("Game Piece Selected: " + hit.transform.name);
                }

            }
        }
        
        if(holdingPiece){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,new Vector3(0,15,17),0.2f);
        }else{
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,new Vector3(0,15,20),0.2f);
        }
        //release The game piece on right click to return it
        if(selectedGamePiece != null && Input.GetMouseButtonDown(1)){
                selectedGamePiece.transform.position = gamePieceStartPos;
                selectedGamePiece = null;
                holdingPiece = false;
                
                setGamePieceLayer(0);

        }
        
        if(selectedGamePiece != null && hit.transform != null){
            //selectedGamePiece.transform.position = Vector3.MoveTowards(selectedGamePiece.transform.position,lastHitPosition,pieceMoveTime);
            if(checkSpot(hit.transform.gameObject)){
            selectedGamePiece.transform.position = hit.transform.position;
            }
            
            if(Input.GetMouseButtonDown(0) && hit.transform != null){
                //Debug.Log(checkSpot(hit.transform.gameObject));
                if(hit.transform.gameObject.CompareTag("GameSpot") && checkSpot(hit.transform.gameObject)){

                    OMG.playAudio(0);
                    try{if(hit.transform.GetChild(0).gameObject != null){GameObject.Destroy(hit.transform.GetChild(0).gameObject);}}
                    catch{}
                    
                    selectedGamePiece.transform.parent = hit.transform;
                    
                    
                    selectedGamePiece = null;
                    holdingPiece = false;
                    //changePlayer();
                    Invoke("changePlayer",1.1f);
                    Invoke("checkGame",0.1f);
                    
                }  
            }
        }
    }
}
    void checkGame(){
        WC.checkGame();
    }
    public void changePlayer(){
        setGamePieceLayer(0);
        Quaternion playerOneRotation = new Quaternion(0,0,0,0);
        Quaternion playerTwoRotation = new Quaternion(0,180,0,0);
        if(PlayerIndex == 1){
            OMG.changePlayer(2);
            try{transform.parent.rotation = Quaternion.Lerp(playerOneRotation,playerTwoRotation,1f * Time.time);}catch{}
            PlayerIndex =2;
        }else{
            try{
                
                transform.parent.rotation = Quaternion.Lerp(playerTwoRotation,playerOneRotation,1f * Time.time);}catch{}

            OMG.changePlayer(1);
            PlayerIndex = 1;
        }
    }
    
    void setGamePieceLayer(int layerIndex)
    {
        allGamePieces = GameObject.FindGameObjectsWithTag("GamePieces");
        foreach(GameObject p in allGamePieces)
        {
            p.layer = layerIndex;
        }
    }

    bool checkGamePiece(GameObject selectedPiece,int playerIndex){
            if(selectedPiece.GetComponent<pieceManager>().ownersIndex == playerIndex){
                return true;
            }else{
                return false;
            }
    }
    bool checkSpot(GameObject selectedSpot){
        pieceManager currentPiece = selectedGamePiece.GetComponent<pieceManager>();

        GameObject spotSelected = selectedSpot;

        pieceManager pieceOnSpot = null;
        
        try{pieceOnSpot = spotSelected.transform.GetChild(0).GetComponent<pieceManager>();}
        catch{}
        
        
            if(pieceOnSpot != null)
            {
                pieceManager pm = pieceOnSpot.GetComponent<pieceManager>();
                if(pieceOnSpot.ownersIndex == PlayerIndex){
                    return false;
                }else{

                Debug.Log("Size of Current Piece: "+ currentPiece.Size +" || Size of Piece on Spot: "+ pieceOnSpot.Size);
                switch(currentPiece.Size){
                    case pieceManager.pieceSize.Small:
                        
                        if(pieceOnSpot.Size != pieceManager.pieceSize.Small&&pieceOnSpot.Size != pieceManager.pieceSize.Medium&&pieceOnSpot.Size != pieceManager.pieceSize.Large){
                            Debug.Log("Small and can be placed");
                            return true;
                        }else{
                            Debug.Log("Small and can not be placed");
                            return false;
                        } 
                    case pieceManager.pieceSize.Medium:
                        
                        if(pieceOnSpot.Size != pieceManager.pieceSize.Medium&&pieceOnSpot.Size != pieceManager.pieceSize.Large){
                            Debug.Log("Medium and can be placed");
                            return true;
                        }else{
                            Debug.Log("Medium and can not be placed");
                            return false;
                        }
                        
                    case pieceManager.pieceSize.Large:
                        if(pieceOnSpot.Size != pieceManager.pieceSize.Large){
                            Debug.Log("Large and can be placed");
                            return true;
                        }else{
                            Debug.Log("Large and can not be placed");
                            return false;
                        }
                }
                return false;
                }
            }else{
                return true;
            }
    }
}

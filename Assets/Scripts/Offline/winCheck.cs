using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class winCheck : MonoBehaviour
{
    //Used to get the playerOnSpot
    public OfflineGameManager OMG;
    public GameObject[] gameSpots;
    public List<int> spotIndex = new List<int>();
    public List<int> playerOnSpot = new List<int>();
    public int[][] winTypes = new int[][]
        {
            //Columns
            new int[]{1,4,7},
            new int[]{2,5,8},
            new int[]{3,6,9},
            //Rows
            new int[]{1,2,3},
            new int[]{4,5,6},
            new int[]{7,8,9},
            //Diagnols
            new int[]{1,5,9},
            new int[]{3,5,7}

        };
    /*
        Spot Reference

        _1_|_2_|_3_
        _4_|_5_|_6_
         7 | 8 | 9

         Win Methods

        -Columns
            1 4 7
            2 5 8
            3 6 9
        -Rows
            1 2 3
            4 5 6
            7 8 9 
        -Diaganols
            1 5 9
            3 5 7
    */
    private void Start() {
        
        gameSpots = GameObject.FindGameObjectsWithTag("GameSpot");
        OMG = GetComponent<OfflineGameManager>();
        foreach(GameObject g in gameSpots){
            spotIndex.Add(Int32.Parse(g.name));
        }
    }
    public void checkGame(){
        int[] player = {1,2};
        //Index of int also referse to the spot reference
        //Clears the list to refresh the information
        playerOnSpot.Clear();
        foreach(GameObject g in gameSpots){
            //Try to get the child of the gameSpot 
            try{
            playerOnSpot.Add(g.transform.GetChild(0).GetComponent<pieceManager>().ownersIndex);
               }
               //If no player on it, set it to 0
               catch{
                    playerOnSpot.Add(0);
               }
        }
        //Winning Algorithm
        bool winner = false;
        foreach(int[] i in winTypes){
            if(!winner){
                foreach(int p in player){
                    if(!winner){
                        if(playerOnSpot[spotIndex.IndexOf(i[0])] == p &&playerOnSpot[spotIndex.IndexOf(i[1])] == p&&playerOnSpot[spotIndex.IndexOf(i[2])] == p){
                            OMG.addScore(p);
                            OMG.playAudio(1);
                        }
                    }
                }
            }
        }
    }
}

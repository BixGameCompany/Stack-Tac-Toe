using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class OfflineGameManager : MonoBehaviour
{
    public int playerOneScore =0;
    private TextMeshProUGUI p1Score{get; set;}

    public int playerTwoScore =0;
    public TextMeshProUGUI p2Score{get; set;}

    public TextMeshProUGUI winText{get; set;}
    public TextMeshProUGUI currentTurn{get; set;}
    public float volume = 0.5f;
    AudioSource As;
    public AudioClip[] soundEffects;
    public GameObject[] playersPieces;

    public Vector3 p1pos;
    public Vector3 p2pos;
    public Slider volSlider;
    
    private void Start() {
        DontDestroyOnLoad(gameObject);
        As = GetComponent<AudioSource>();
    }
    private void Update() {
        p1Score.text = playerOneScore.ToString();
        p2Score.text = playerTwoScore.ToString();
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<OfflinePlayerController>().isPaused){
            volume = volSlider.value;
        }
    }
    
    public void changePlayer(int playerIndex){
        currentTurn.text = "P"+playerIndex.ToString()+"'s Turn";
        if(playerIndex ==1){
            currentTurn.color = new Color(0,150,255,255);
        }else if(playerIndex == 2){
            currentTurn.color = new Color(255,160,0,255);
        }else{

        }
    }
    public void addScore(int playerIndex){
        if(playerIndex == 1){
            playerOneScore += 1;
            winText.enabled = true;
            winText.text = "Player 1 Win!";
            Invoke("ResetBoard",1f);
        }else if(playerIndex == 2){
            playerTwoScore += 1;
            winText.enabled = true;
            winText.text = "Player 2 Win!";
            Invoke("ResetBoard",1f);
        }else if(playerIndex == 3){
            winText.enabled = true;
            winText.text = "Draw!";
            Invoke("ResetBoard",1f);
        }
    }
    public void playAudio(int soundIndex){
        As.PlayOneShot(soundEffects[soundIndex],volume);
    }
    void ResetBoard(){
        winText.enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<OfflinePlayerController>().changePlayer();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("GamePieces")){
            try{Destroy(g);}catch{}
        }
        Instantiate(playersPieces[0],p1pos,Quaternion.identity);
        Instantiate(playersPieces[1],p2pos,Quaternion.Euler(0,180,0));
    }
    public void quitGame(){
        Application.Quit();
    }
}

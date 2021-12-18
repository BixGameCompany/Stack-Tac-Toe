using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Title;
    
    void Start()
    {
        Invoke("playTitle",1f);
    }
    void playTitle(){
        Title.GetComponent<Animation>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeLevel(int levelIndex){
        SceneManager.LoadScene(1);
    }
    public void quitGame(){
        Application.Quit();
    }
}

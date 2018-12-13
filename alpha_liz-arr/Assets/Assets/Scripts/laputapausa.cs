using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class laputapausa : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject pausemenu;
    public GameObject optionsmenu;
    

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        optionsmenu.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;

    }

    void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;

         
    }

    public void Loadmenu()
    {
        Time.timeScale = 0f;
        optionsmenu.SetActive(true);
        pausemenu.SetActive(false);



    }

    public void Quitgame()
    {
        
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("MenuPuestadeSol");

    }
    
    

}
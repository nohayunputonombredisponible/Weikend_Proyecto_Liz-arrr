using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Inputmanager inputManager;
   
    public int score;

    private void Awake()
    {
        inputManager = GetComponent<Inputmanager>();
       

        score = 0;
        
    }

    public void Pause()
    {
        inputManager.SetPause(true);
        
        Time.timeScale = 0;
    }

    public void Resume()
    {
        inputManager.SetPause(false);
        
        Time.timeScale = 1;
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void AddScore(int value)
    {
        score += value;
      
    }

    public void GameOver()
    {
        SaveGame();
        LoadScene(2);
    }

    private void SaveGame()
    {
        //Guardar score y highscore

        int highScore = 0;

        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        if(score > highScore)
        {
            highScore = score;
        }

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("HighScore", highScore);
    }

  
}


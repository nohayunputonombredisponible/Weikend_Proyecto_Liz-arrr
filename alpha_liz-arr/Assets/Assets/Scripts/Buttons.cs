using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void LoadScene(int num)
    {
        Debug.Log("Load scene: " + num);
        SceneManager.LoadScene(num);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

}

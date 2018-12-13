using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scenes : MonoBehaviour
{
    public float tiempo = 8;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Temp());

    }

    // Update is called once per frame
    IEnumerator Temp()
    {
        yield return new WaitForSeconds(tiempo);
        SceneManager.LoadScene("MenuPuestadeSol");

    }
}

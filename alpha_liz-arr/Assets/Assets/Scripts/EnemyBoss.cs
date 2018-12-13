using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour
 {
	 

    public int health;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            LoadScene();
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
    }

		 public void LoadScene()
    {
        
        SceneManager.LoadScene("endingescene");
    }  
	

}



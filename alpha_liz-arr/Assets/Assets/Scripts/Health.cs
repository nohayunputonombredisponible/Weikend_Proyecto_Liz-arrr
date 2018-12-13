using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static float health = 4;
    public int numofhearts;

    public GameObject deathpanel;
    public float tiempo = 0.5f;

    public GameObject player;
    public GameObject RespawnPoint;
    public Animator anim;



    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyhearth;

    void start()
    {
       
    }

    void Update()
    {

        if(health > numofhearts)

        {
            health = numofhearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyhearth;
            }


            if(i < numofhearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = true;

            }
        }

        if(health <= 0)
        {
            Death();
        }
      

        if (Input.GetKeyDown(KeyCode.Q))
        {
            health ++;
        }

        

        
        
    }


    public void Death()
    {
        StartCoroutine(Temp());
        anim.SetBool("isdeath", true);
        health = 5;




    }

    public void Respawn()
    {
        player.transform.position = RespawnPoint.transform.position;
        

    }

    IEnumerator Temp()
    {
        yield return new WaitForSeconds(tiempo);
        Respawn();
        anim.SetBool("isdeath", false);
        



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "mar")
        {
            Death();
            
        }
    }







}

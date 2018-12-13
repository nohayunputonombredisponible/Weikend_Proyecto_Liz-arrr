using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogoterminado : MonoBehaviour
{
   public TextMeshProUGUI textDisplay;
   public string[] sentences;
    private int index;
    public float typingspeed;
    public GameObject dialoguebox;
    public bool intrigger = false;
    public bool dialogueactive = false;
    public GameObject indicacion;
    
    public bool candisplay = false;

   static public bool queststarted = false;


    

     

 

    private void Start()
    {
        

    }

    private void Update()
    {
        if (intrigger)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCoroutine(Type());
                intrigger = false;

                
                
                


            }
            indicacion.SetActive(true);


        }

        if(dialogueactive)
        {

            if (Input.GetKeyDown(KeyCode.B))
            {
                Passsentence();             
            }


        }
        
        if (textDisplay.text == sentences[index] && candisplay )
        {
            dialogueactive = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            
              intrigger = true;
              candisplay = true;
              
             
              
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {


            intrigger = false;
            indicacion.SetActive(false);
            candisplay = false;
            dialogueactive = false;



        }
    }

 

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            dialoguebox.SetActive(true);
            
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingspeed);
        }
    }

    public void NextSentence()
    {

        dialogueactive = false;

        if (index < sentences.Length - 1 )
        {
            if(Puzzle.favored == false)
            {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            indicacion.SetActive(false);
            }

        
            


        }
        else
        {
            textDisplay.text = "";
            dialoguebox.SetActive(false);
            intrigger = false;
            index = 0;
            dialogueactive = false;
            indicacion.SetActive(false);
            if(Puzzle.favored == false)
            {
                  queststarted = true;
            }
           


        }

        
        

    }

    public void Passsentence()
    {
       
        
            NextSentence();
        
    }

   
}

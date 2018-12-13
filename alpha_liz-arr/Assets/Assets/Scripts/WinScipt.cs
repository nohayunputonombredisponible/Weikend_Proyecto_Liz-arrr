using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScipt : MonoBehaviour
 {
	 public GameObject Victorypanel;

	// Use this for initialization
	void Start () 
	{
		Victorypanel.SetActive(false);
		
	}
	
   
	// Update is called once per frame
	 private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {     
			Victorypanel.SetActive(true);
			Time.timeScale = 0;
    	
	           
		}        
   }   

   

}

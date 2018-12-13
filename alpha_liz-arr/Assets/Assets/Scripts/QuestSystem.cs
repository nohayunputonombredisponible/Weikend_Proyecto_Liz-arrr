using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
 {

	 public GameObject puzzleeee;
	 public GameObject audio;

	 public Camera Maincam;
     public Camera Puzzlecam;

	 public GameObject puzzlepanel;

	 public GameObject favor;

	 


	// Use this for initialization
	void Start () 
	{
		Puzzlecam.gameObject.SetActive(false);
		favor.gameObject.SetActive(false);
		
		
	}
	
	// Update is called once per frame
	void Update ()
	 {
		 if(Dialogoterminado.queststarted == true)
		 {	
		 	Puzzlecam.gameObject.SetActive(true);
         	Maincam.gameObject.SetActive(false);
         	puzzlepanel.gameObject.SetActive(true);
            puzzleeee.gameObject.SetActive(true);
			audio.gameObject.SetActive(false);
		 }

		 if(Puzzle.favored == true)
		 {
			favor.gameObject.SetActive(true); 
			audio.gameObject.SetActive(true);
		 }
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputmanager : MonoBehaviour
{
    private Playercontroller playercontroller;
    private float sensitibity = 3.0f;
    
  
    private GameManager gamemanager;
    private bool isPaused = false;

    private Pausecursor mousecursor;

    

    



	// Use this for initialization
	void Start ()
    {
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<Playercontroller>();
        

        

        mousecursor = new Pausecursor();
        mousecursor.HideCursor();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //movimiento del player

        if(Input.GetKeyDown(KeyCode.P))
        {
            //Gamemanager pausa el juego
            if(!isPaused) gamemanager.Pause();
            else gamemanager.Resume();
        }

        if(isPaused) return;

        Vector2 inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
        playercontroller.SetAxis(inputAxis);

        Vector2 inputaxis = Vector2.zero;
        inputaxis.x = Input.GetAxis("Horizontal");
        inputaxis.y = Input.GetAxis("Vertical");
        playercontroller.SetAxis(inputaxis);

        //salto del player

        if(Input.GetButton("Jump")) playercontroller.StartJump();

        //movimiento de la camara

        Vector2 mouseAxis = Vector2.zero;
        mouseAxis.x = Input.GetAxis("Mouse X") * sensitibity ;
        mouseAxis.y = Input.GetAxis("Mouse Y") * sensitibity ;
        

        if(Input.GetMouseButtonDown(0)) mousecursor.HideCursor();
        else if(Input.GetKeyDown(KeyCode.Escape)) mousecursor.ShowCursor();

        


       




    }
    public void SetPause(bool p)
    {
        isPaused = p;
    }

    
}

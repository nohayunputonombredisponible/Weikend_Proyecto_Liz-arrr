using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playercontroller : MonoBehaviour
{

    private CharacterController controller;
    
    private Vector2 axis;
    public float speed;
    public Vector3 movedirection;
    private float forceroGround = Physics.gravity.y;

    public float hitYourself;
    public float maxEnergy;
    public float energy;
    public float maxStamina;
    

    public int life;




    public float jumpspeed;
    private bool jump;
    public float gravitymagnitude;
 
    public Animator hitAnim;
    public Animator attack;



    // Use this for initialization
    void Start()
    {

        controller = GetComponent<CharacterController>();
       

    }

    // Update is called once per frame
    void Update()
    {

        
        

        if (controller.isGrounded && !jump)
        {
            movedirection.y = forceroGround;

        }
        else
        {
            jump = false;
            movedirection.y += Physics.gravity.y * gravitymagnitude * Time.deltaTime;
        }

        Vector3 transformDirection = axis.x * transform.right + axis.y * transform.forward;

        movedirection.x = transformDirection.x * speed;
        movedirection.z = transformDirection.z * speed;

        controller.Move(movedirection * Time.deltaTime);

    }

    public void SetAxis(Vector2 inputaxis)
    {
        axis = inputaxis;

      
    }

    public void StartJump()
    {
        if(!controller.isGrounded) return;

        movedirection.y = jumpspeed;
        jump = true;
    }


    public void ReceivedDamage(int hit)
    {
              

        hitAnim.SetTrigger("Hit");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            life--;
        }
    }

    
    
    

}
 

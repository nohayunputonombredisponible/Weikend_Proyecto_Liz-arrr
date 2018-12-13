using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewcharacterController : MonoBehaviour
{

    public float floorOffsetY;
    public float moveSpeed = 6f;
    public float rotateSpeed = 10f;
    public float jumpforce = 2f;
    private bool Ultrajump;
    private bool isjumping = false;

    public int damage = 0;

    public bool godmode = false;


    public float gravityjump = 1.2f;

    public float jumpspeed = 5000f;

    public float Currentjump = 0;

    public string animationFloatname;

    private AudioSource audio;

    Rigidbody rb;
    public Animator anim;
    float vertical;
    float horizontal;
    Vector3 moveDirection;
    float inputAmount;
    Vector3 raycastFloorPos;
    Vector3 floorMovement;
    Vector3 gravity;
    Vector3 CombinedRaycast;
    Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // reset movement
        moveDirection = Vector3.zero;
        // get vertical and horizontal movement input (controller and WASD/ Arrow Keys)
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");



        // base movement on camera
        Vector3 correctedVertical = vertical * Camera.main.transform.forward;
        Vector3 correctedHorizontal = horizontal * Camera.main.transform.right;

        Vector3 combinedInput = correctedHorizontal + correctedVertical;
        // normalize so diagonal movement isnt twice as fast, clear the Y so your character doesnt try to
        // walk into the floor/ sky when your camera isn't level
        moveDirection = new Vector3((combinedInput).normalized.x, 0, (combinedInput).normalized.z);

        // make sure the input doesnt go negative or above 1;
        float inputMagnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        inputAmount = Mathf.Clamp01(inputMagnitude);

        // rotate player to movement direction
        Quaternion rot = Quaternion.LookRotation(moveDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * inputAmount * rotateSpeed);
        transform.rotation = targetRotation;

        // handle animation blendtree for walking
        //anim.SetFloat(animationFloatname, inputAmount, 0.2f, Time.deltaTime);
        anim.SetBool("Run", moveDirection.magnitude > 0);

        if (godmode == true)
        {
            Godmode();
        }






    }


    private void FixedUpdate()
    {
        
        // if not grounded , increase down force
        if (FloorRaycasts(0, 0, 0.6f) == Vector3.zero && godmode == false) 
        {
            isjumping = false;
            gravity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
            // anim.SetBool("is_in_air", true);

            if ( Input.GetKeyDown("space") && godmode == false && Currentjump == 0)
            {



                gravity.y = 4;
                rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
                Currentjump++;
                audio.Play();

            }

            if ((Input.GetKeyDown("space") && Currentjump == 1 ) && godmode == false)
            {

                gravity.y = 4;
                rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
                Currentjump++;
                audio.Play();
            }



        }

        // actual movement of the rigidbody + extra down force
        rb.velocity = (moveDirection * moveSpeed * inputAmount) + gravity;

        // find the Y position via raycasts
        floorMovement = new Vector3(rb.position.x, FindFloor().y + floorOffsetY, rb.position.z);

        // only stick to floor when grounded
        if (FloorRaycasts(0, 0, 0.6f) != Vector3.zero && floorMovement != rb.position)
        {
            // move the rigidbody to the floor
            rb.MovePosition(floorMovement);
            gravity.y = 0;
            //anim.SetBool("is_in_air", false);
            Currentjump = 0;
            moveSpeed = 6f;


        }

        if (FloorRaycasts(0, 0, 0.6f) != Vector3.zero && Input.GetKeyDown("space") && godmode == false)
        {
            
            
                
                gravity.y = 4;
                rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
                Currentjump++;
                audio.Play();           

           
        }


        if ((Input.GetKeyDown("space") && Currentjump <= 1 && FloorRaycasts(0, 0, 0.6f) == Vector3.zero) && godmode == false)
        {
            
            gravity.y = 4;
            rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
            Currentjump++;
            audio.Play();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            godmode = !godmode;

        }

     








    }

    void jump()
    {


        //rb.velocity = new Vector3(0,jumpspeed,0) ;
        gravity.y = 4;
        rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
       
        //anim.SetBool("is_in_air", true);   
        
        



    }

    void Godmode()
    {
        if(Input.GetKey("space"))
        {
            moveDirection.y++;
        }
        if (Input.GetKey(KeyCode.X))
        {
            moveDirection.y--;
        }

        moveSpeed = 12;
        Health.health = 5;
        damage = 5;



    }

    private void OnTriggerEnter(Collider other)
    {
        if (godmode == true)
        {


            if (other.CompareTag("tronco"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            }


            if (other.CompareTag("Boss"))
            {
                BoosHealth.healthofboss -= 100;
            }
        }


    }




        Vector3 FindFloor()
    {
        // width of raycasts around the centre of your character
        float raycastWidth = 0.25f;
        // check floor on 5 raycasts   , get the average when not Vector3.zero  
        int floorAverage = 1;

        CombinedRaycast = FloorRaycasts(0, 0, 1.6f);
        floorAverage += (getFloorAverage(raycastWidth, 0) + getFloorAverage(-raycastWidth, 0) + getFloorAverage(0, raycastWidth) + getFloorAverage(0, -raycastWidth));

        return CombinedRaycast / floorAverage;


    }

    // only add to average floor position if its not Vector3.zero
    int getFloorAverage(float offsetx, float offsetz)
    {

        if (FloorRaycasts(offsetx, offsetz, 1.6f) != Vector3.zero)
        {
            CombinedRaycast += FloorRaycasts(offsetx, offsetz, 1.6f);
            return 1;
        }
        else { return 0; }
    }


    Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
    {
        RaycastHit hit;
        // move raycast
        raycastFloorPos = transform.TransformPoint(0 + offsetx, 0 + 0.5f, 0 + offsetz);

        Debug.DrawRay(raycastFloorPos, Vector3.down, Color.magenta);
        if (Physics.Raycast(raycastFloorPos, -Vector3.up, out hit, raycastLength))
        {
            return hit.point;
        }
        
        else return Vector3.zero;

    }

}